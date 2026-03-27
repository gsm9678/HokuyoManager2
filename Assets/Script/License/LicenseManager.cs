using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

[Serializable]
public class LicenseData
{
    public string hwid;          // RequestCode(16자)
    public string activatedAtUtc;
    public string features;      // 필요하면 "FULL"/"DEMO"
}

public static class LicenseManager
{
    private const string LicenseFileName = "license.dat";

    // 이 SECRET은 Unity와 KeyGen이 "완전히 동일"해야 함 (절대 외부 공개 X)
    // 가능하면 길고 랜덤한 문자열로 바꾸기
    private const string SECRET = "bxNCb1bvj1AO6+dZnB0OE4W/IcGk4a1Pbhmy4SbdLFjtxpRlY7cmblzOddxtKmhJ65CnBm10XzSqT/B1Va9/YcuPNgq/rrJaU+vl+a7eOnBZYESlvGH2OIa4aonCmIr2UVDBivNXNFfmfZhUgxggVYs4bGKVb2WXIMzRctywQuYHY0MCzKN1po2EKeVnojVKZzom+z1Bms2qZrtO5HPVCo7kryFvv3pHIssWUzfmK3zms57qD1GuhyIFBq6nu3a+5OwRvj14aXKj4XPcfNBfO9f4XE2jO9NRTSV5ni7n51E1FiCsAOXRohGfhgfgdRjINKwzc1YKxklSqTrx5CZboQ";

    // ActivationKey 길이 (사람 입력용)
    private const int ActivationKeyLength = 20; // 16~24 권장

    public static bool IsLicensed { get; private set; }
    public static LicenseData Current { get; private set; }
    public static string LastError { get; private set; }

    public static string GetRequestCode()
    {
        return HardwareId.GetHardwareFingerprint(); // 16자
    }

    public static bool LoadAndValidate()
    {
        LastError = "";
        IsLicensed = false;
        Current = null;

        string path = GetLicensePath();
        if (!File.Exists(path))
        {
            LastError = "라이선스 파일이 없습니다.";
            return false;
        }

        try
        {
            string json = File.ReadAllText(path, Encoding.UTF8);
            var data = JsonUtility.FromJson<LicenseData>(json);

            if (data == null || string.IsNullOrEmpty(data.hwid))
            {
                LastError = "라이선스 파일이 손상되었습니다.";
                return false;
            }

            // HWID 매칭 (다른 PC로 복사 방지)
            string currentHwid = GetRequestCode();
            if (!string.Equals(data.hwid, currentHwid, StringComparison.OrdinalIgnoreCase))
            {
                LastError = "이 PC에 발급된 라이선스가 아닙니다(HWID 불일치).";
                return false;
            }

            IsLicensed = true;
            Current = data;
            return true;
        }
        catch (Exception e)
        {
            LastError = "라이선스 로드 실패: " + e.Message;
            return false;
        }
    }

    public static bool Activate(string activationKeyInput)
    {
        LastError = "";

        string input = (activationKeyInput ?? "").Trim().ToUpperInvariant();
        if (string.IsNullOrEmpty(input))
        {
            LastError = "Activation Key가 비어있습니다.";
            return false;
        }

        string requestCode = GetRequestCode();
        string expected = GenerateActivationKey(requestCode);

        if (!ConstantTimeEquals(input, expected))
        {
            LastError = "Activation Key가 올바르지 않습니다.";
            return false;
        }

        // 검증 성공 → 라이선스 저장
        var data = new LicenseData
        {
            hwid = requestCode,
            activatedAtUtc = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ"),
            features = "FULL"
        };

        try
        {
            File.WriteAllText(GetLicensePath(), JsonUtility.ToJson(data), Encoding.UTF8);
            IsLicensed = true;
            Current = data;
            return true;
        }
        catch (Exception e)
        {
            LastError = "라이선스 저장 실패: " + e.Message;
            return false;
        }
    }

    // ===== 핵심: ActivationKey 생성 =====
    public static string GenerateActivationKey(string requestCode16)
    {
        // HMAC-SHA256(SECRET, requestCode)
        byte[] key = Encoding.UTF8.GetBytes(SECRET);
        byte[] msg = Encoding.UTF8.GetBytes(requestCode16);

        byte[] hash;
        using (var hmac = new HMACSHA256(key))
            hash = hmac.ComputeHash(msg);

        // Base32로 인코딩해서 사람이 치기 좋게 (A-Z2-7)
        string base32 = ToBase32NoPadding(hash);

        // 원하는 길이로 절단
        return base32.Substring(0, ActivationKeyLength);
    }

    private static string GetLicensePath()
        => Path.Combine(Application.persistentDataPath, LicenseFileName);

    // ===== 유틸: Base32 =====
    private static string ToBase32NoPadding(byte[] data)
    {
        const string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ234567";
        if (data == null || data.Length == 0) return "";

        int outputLen = (int)Math.Ceiling(data.Length / 5d) * 8;
        var sb = new StringBuilder(outputLen);

        int buffer = data[0];
        int next = 1;
        int bitsLeft = 8;

        while (bitsLeft > 0 || next < data.Length)
        {
            if (bitsLeft < 5)
            {
                if (next < data.Length)
                {
                    buffer <<= 8;
                    buffer |= data[next++] & 0xFF;
                    bitsLeft += 8;
                }
                else
                {
                    int pad = 5 - bitsLeft;
                    buffer <<= pad;
                    bitsLeft += pad;
                }
            }

            int index = (buffer >> (bitsLeft - 5)) & 0x1F;
            bitsLeft -= 5;
            sb.Append(alphabet[index]);
        }

        return sb.ToString();
    }

    // ===== 유틸: 타이밍 공격 방지 비교 =====
    private static bool ConstantTimeEquals(string a, string b)
    {
        if (a == null || b == null) return false;
        if (a.Length != b.Length) return false;

        int diff = 0;
        for (int i = 0; i < a.Length; i++)
            diff |= a[i] ^ b[i];

        return diff == 0;
    }
}

using System;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

public static class HardwareId
{
    // 요청코드 길이 (원하는 값)
    private const int RequestCodeLength = 16;

    public static string GetRequestCode16()
    {
        // 기존 지문 재료
        string volume = GetSystemVolumeSerial();
        string mac = GetPrimaryMac();
        string cpu = SystemInfo.processorType ?? "";
        string gpu = SystemInfo.graphicsDeviceName ?? "";

        string raw = $"VOL:{volume}|MAC:{mac}|CPU:{cpu}|GPU:{gpu}";

        // SHA256 바이트 -> Base32 -> 12자
        byte[] hash = Sha256Bytes(raw);
        string base32 = ToBase32NoPadding(hash); // A-Z2-7
        return base32.Substring(0, RequestCodeLength);
    }

    // (라이선스 payload에는 12자를 넣을 거면 이 값 사용)
    public static string GetHardwareFingerprint() => GetRequestCode16();

    // --- C 드라이브 시리얼 ---
    private static string GetSystemVolumeSerial()
    {
        try
        {
            if (!GetVolumeInformation("C:\\", null, 0, out uint serial, out _, out _, null, 0))
                return "0";
            return serial.ToString("X8");
        }
        catch { return "0"; }
    }

    [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
    private static extern bool GetVolumeInformation(
        string rootPathName,
        StringBuilder volumeNameBuffer,
        int volumeNameSize,
        out uint volumeSerialNumber,
        out uint maximumComponentLength,
        out uint fileSystemFlags,
        StringBuilder fileSystemNameBuffer,
        int fileSystemNameSize
    );

    // --- MAC 주소 ---
    private static string GetPrimaryMac()
    {
        try
        {
            foreach (var nic in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (nic.OperationalStatus != OperationalStatus.Up) continue;
                if (nic.NetworkInterfaceType == NetworkInterfaceType.Loopback) continue;

                var mac = nic.GetPhysicalAddress()?.ToString();
                if (!string.IsNullOrEmpty(mac))
                    return mac;
            }
        }
        catch { }
        return "00";
    }

    private static byte[] Sha256Bytes(string s)
    {
        using (var sha = SHA256.Create())
        {
            return sha.ComputeHash(Encoding.UTF8.GetBytes(s));
        }
    }

    // Base32 (RFC 4648) - padding 제거
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

        // padding '=' 없이 반환
        return sb.ToString();
    }
}

using UnityEngine;
using UnityEngine.UI;

public class LicenseGate : MonoBehaviour
{
    [Header("UI")]
    public GameObject panel;
    public Text requestCodeText;
    public InputField activationInput;
    public Text statusText;
    public Button CopyButton;
    public Button ConfirmButton;

    [Header("Licensed Objects")]
    public GameObject[] enableWhenLicensed;

    private void Start()
    {
        ApplyState(LicenseManager.LoadAndValidate());
        CopyButton.onClick.AddListener(delegate { CopyRequestCodeToClipboard(); });
        ConfirmButton.onClick.AddListener(delegate { OnClickActivate(); } );
    }

    private void ApplyState(bool ok)
    {
        panel.SetActive(!ok);

        foreach (var go in enableWhenLicensed)
            if (go) go.SetActive(ok);

        if (!ok)
        {
            requestCodeText.text = LicenseManager.GetRequestCode();
            statusText.text = LicenseManager.LastError;
        }
        else
        {
            statusText.text = "라이선스 인증 완료";
        }
    }

    public void OnClickActivate()
    {
        bool ok = LicenseManager.Activate(activationInput.text);
        statusText.text = ok ? "활성화 성공! 재시작 없이 적용됨" : LicenseManager.LastError;
        ApplyState(ok);
    }

    public void CopyRequestCodeToClipboard()
    {
        GUIUtility.systemCopyBuffer = LicenseManager.GetRequestCode();
        statusText.text = "요청코드를 클립보드에 복사했습니다.";
    }
}

using HuaweiMobileServices.Id;
using HuaweiMobileServices.Utils;
using UnityEngine;
using TMPro;
using HmsPlugin;

public class AccountSignIn : MonoBehaviour
{
    private const string NOT_LOGGED_IN = "No user logged in";
    private const string LOGGED_IN = "welcome, {0}!";
    private const string LOGIN_ERROR = "Error or cancelled login";

    [SerializeField] TextMeshProUGUI loggedInUser;
    private AccountManager accountManager;

    // Start is called before the first frame update
    void Start()
    {
        loggedInUser = GetComponent<TextMeshProUGUI>();
        loggedInUser.text = NOT_LOGGED_IN;

        accountManager = AccountManager.GetInstance();
        accountManager.OnSignInSuccess = OnLoginSuccess;
        accountManager.OnSignInFailed = OnLoginFailure;
        LogIn();
    }

    public void LogIn()
    {
        accountManager.SignIn();
    }

    public void LogOut()
    {
        accountManager.SignOut();
        loggedInUser.text = NOT_LOGGED_IN;
    }

    public void OnLoginSuccess(AuthHuaweiId authHuaweiId)
    {
        loggedInUser.text = string.Format(LOGGED_IN, authHuaweiId.DisplayName);
    }

    public void OnLoginFailure(HMSException error)
    {
        loggedInUser.text = LOGIN_ERROR;
    }
}

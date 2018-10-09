using UnityEngine;
using GetSocialSdk.Ui;
using GetSocialSdk.Core;

public class LevelManager : MonoBehaviour
{

    public void LoadLevel(string name)
    {
        Debug.Log("Level load requested for " + name);
#pragma warning disable CS0618 // Type or member is obsolete
        Application.LoadLevel(name);
#pragma warning restore CS0618 // Type or member is obsolete
        GetSocial.Init();
        bool wasShown = GetSocialUi.CreateInvitesView().Show();
        Debug.Log("Smart Invites view was shown: " + wasShown);
        GetSocial.SendInvite(InviteChannelIds.Email,
    onComplete: () => Debug.Log("Invitation via EMAIL was sent"),
    onCancel: () => Debug.Log("Invitation via EMAIL was cancelled"),
    onFailure: (error) => Debug.LogError("Invitation via EMAIL failed, error: " + error.Message)
);

    }

}
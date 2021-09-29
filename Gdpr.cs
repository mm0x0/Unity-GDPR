using UnityEngine;
using System;

// 回答の種類
public enum GdprAnswer
{
    UNANSWERD,
    CONSENT,
    NOT_CONSENT,
}

public class Gdpr : MonoBehaviour
{
    // 回答保存キー
    protected static readonly string STATUS_KEY = "GdprStatus";

    // GDPRステータスを取得
    GdprAnswer GetStatus ()
    {
        // 回答してないときはUNANSWERDを記録
        if (!PlayerPrefs.HasKey (STATUS_KEY))
            PlayerPrefs.SetString (STATUS_KEY, GdprAnswer.UNANSWERD.ToString ());

        // ステータスを文字列で取得
        string answerStr = PlayerPrefs.GetString (STATUS_KEY);

        // 文字列からEnumに変換
        GdprAnswer answer = (GdprAnswer) Enum.Parse (typeof (GdprAnswer), answerStr);

        return answer;
    }

    // フォームに回答（YES）
    public void UserConsentGdpr ()
        => PlayerPrefs.SetString (STATUS_KEY, (GdprAnswer.CONSENT).ToString ());

    // フォームに回答（NO）
    public void UserNotConsentGdpr ()
        => PlayerPrefs.SetString (STATUS_KEY, (GdprAnswer.NOT_CONSENT).ToString ());

    // ユーザーが回答をしたか取得
    public bool IsUserAnswerdGdpr ()
        => GetStatus () != GdprAnswer.UNANSWERD;

    // ユーザーがGDPRに同意したか取得
    public bool IsUserConsentGdpr ()
        => GetStatus () == GdprAnswer.CONSENT;
}
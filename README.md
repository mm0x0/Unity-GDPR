# GDPRとは？

EU圏に在住する人の個人データ（名前、Eメールなど）の保護に関する国際法で、違反すると罰金が課せられるらしい

Admobの参考ページ：[EU ユーザーからの同意の取得](https://developers.google.com/admob/unity/eu-consent?hl=ja)


# Gdpr.cs

## やってること
PlayerPrefsに"GDPRStatus"のキーで同意状態を保存しています

| 値 | 内容 |
----|---- 
| UNANSWERD | 未回答 |
| CONSENT | GDPRに同意した |
| NOT_CONSENT | GDPRに同意しなかった|

## 使い方

### メソッド
| メソッド | 内容 |
----|---- 
| UserConsentGdpr () | YESに回答する |
| UserNotConsentGdpr () | NOに回答する |
| IsUserAnswerdGdpr () | ユーザーが回答をしたか取得|
| IsUserConsentGdpr () | ユーザーがGDPRに同意したか取得|

### 同意フォームの作成

1. YesボタンとNoボタンのあるフォームを作成する。
![1](https://user-images.githubusercontent.com/26345138/135223979-29558d12-4731-47a7-b2a5-7017c2d1fc7a.png)

2. AddComponent > GdprでGdprをアタッチ。
![2](https://user-images.githubusercontent.com/26345138/135223152-8195d5ed-9741-4ee7-955e-e84adda6a1ac.png)

3. Yesボタンを押したときGdprのUserConsentGdpr (), Noボタンを押したときにGdprのUserNotConsentGdpr ()を実行させるようにする。（ボタンを押したあとフォームを非表示にしています）
![3](https://user-images.githubusercontent.com/26345138/135224974-4cb01dd3-0add-4123-9a9d-d504fec120e9.png)

### 同意フォームの表示実装例

Awake時に未回答のときだけフォームを表示する例

Main.cs
```
using UnityEngine;

public class Main : MonoBehaviour
{
    [SerializeField] Gdpr gdpr;
    [SerializeField] GameObject gdprForm;

    void Awake ()
    {
        ShowGdprForm ();
    }

    // GDPRフォームを表示
    void ShowGdprForm ()
    {
        // 必要なら表示する国を判別
    	if (Application.systemLanguage == SystemLanguage.Japanese)
            return;

        // GDPRダイアログを表示（未回答のとき）
        if (!gdpr.IsUserAnswerdGdpr ())
    	   gdprForm.SetActive (true);
    }
}
```

### Admobの実装

```
// ユーザーが同意したかを取得して
if (gdpr.IsUserConsentGdpr ()
{
    // 同意したときは普通に表示
    bannerView.LoadAd (new AdRequest.Builder ().Build ());
}
else
{
    // 同意してないときはパーソナライズされてない広告を表示
    bannerView.LoadAd (new AdRequest.Builder ().AddExtra ("npa", "1").Build ());
}
```
参考ページ：[EU ユーザーからの同意の取得 ](https://developers.google.com/admob/unity/eu-consent?hl=ja#forward_consent_to_the_google_mobile_ads_sdk)

<h1 align="center">Fancy Carousel View</h1>

[![license](https://img.shields.io/badge/LICENSE-MIT-green.svg)](LICENSE.md)

[English Documents Available(英語ドキュメント)](README.md)

<a href="https://github.com/setchi/FancyScrollView">Fancy Scroll View</a>を使用した、UnityのuGUI用のカルーセルビューです。

<p align="center">
  <img width=700 src="https://user-images.githubusercontent.com/47441314/136406607-a3bc489f-2c77-40bc-bc6d-d2858f82a73c.gif" alt="Demo">
</p>

## 目次
<!-- START doctoc generated TOC please keep comment here to allow auto update -->
<!-- DON'T EDIT THIS SECTION, INSTEAD RE-RUN doctoc TO UPDATE -->
<!-- param::isNotitle::true:: -->

- [特徴](#%E7%89%B9%E5%BE%B4)
- [デモ](#%E3%83%87%E3%83%A2)
- [セットアップ](#%E3%82%BB%E3%83%83%E3%83%88%E3%82%A2%E3%83%83%E3%83%97)
    - [要件](#%E8%A6%81%E4%BB%B6)
    - [インストール](#%E3%82%A4%E3%83%B3%E3%82%B9%E3%83%88%E3%83%BC%E3%83%AB)
- [基本的な使い方](#%E5%9F%BA%E6%9C%AC%E7%9A%84%E3%81%AA%E4%BD%BF%E3%81%84%E6%96%B9)
    - [セルのためのデータを作成する](#%E3%82%BB%E3%83%AB%E3%81%AE%E3%81%9F%E3%82%81%E3%81%AE%E3%83%87%E3%83%BC%E3%82%BF%E3%82%92%E4%BD%9C%E6%88%90%E3%81%99%E3%82%8B)
    - [セルを作成する](#%E3%82%BB%E3%83%AB%E3%82%92%E4%BD%9C%E6%88%90%E3%81%99%E3%82%8B)
    - [カルーセルビューを作成する](#%E3%82%AB%E3%83%AB%E3%83%BC%E3%82%BB%E3%83%AB%E3%83%93%E3%83%A5%E3%83%BC%E3%82%92%E4%BD%9C%E6%88%90%E3%81%99%E3%82%8B)
    - [カルーセルビューを初期化する](#%E3%82%AB%E3%83%AB%E3%83%BC%E3%82%BB%E3%83%AB%E3%83%93%E3%83%A5%E3%83%BC%E3%82%92%E5%88%9D%E6%9C%9F%E5%8C%96%E3%81%99%E3%82%8B)
- [応用的な使い方](#%E5%BF%9C%E7%94%A8%E7%9A%84%E3%81%AA%E4%BD%BF%E3%81%84%E6%96%B9)
    - [Carousel Viewの各プロパティを理解する](#carousel-view%E3%81%AE%E5%90%84%E3%83%97%E3%83%AD%E3%83%91%E3%83%86%E3%82%A3%E3%82%92%E7%90%86%E8%A7%A3%E3%81%99%E3%82%8B)
    - [プログレスビューを使用する](#%E3%83%97%E3%83%AD%E3%82%B0%E3%83%AC%E3%82%B9%E3%83%93%E3%83%A5%E3%83%BC%E3%82%92%E4%BD%BF%E7%94%A8%E3%81%99%E3%82%8B)
    - [セルの動きをカスタムする](#%E3%82%BB%E3%83%AB%E3%81%AE%E5%8B%95%E3%81%8D%E3%82%92%E3%82%AB%E3%82%B9%E3%82%BF%E3%83%A0%E3%81%99%E3%82%8B)
    - [スクロールビューと併せて使う](#%E3%82%B9%E3%82%AF%E3%83%AD%E3%83%BC%E3%83%AB%E3%83%93%E3%83%A5%E3%83%BC%E3%81%A8%E4%BD%B5%E3%81%9B%E3%81%A6%E4%BD%BF%E3%81%86)
- [ライセンス](#%E3%83%A9%E3%82%A4%E3%82%BB%E3%83%B3%E3%82%B9)

<!-- END doctoc generated TOC please keep comment here to allow auto update -->

## 特徴
* カルーセルビューを数ステップで簡単に作成可能
* 表示に不要なセルが使いまわされるため高パフォーマンス
* 縦/横スクロールに対応
* カルーセルの動き・各パラメータは細かくカスタム可能

## デモ
1. リポジトリをクローンする
2. 以下のシーンを開いて再生する
    * https://github.com/Haruma-K/FancyCarouselView/blob/master/Assets/Demo/Scenes/CarouselDemo.unity

## セットアップ

#### 要件
Unity 2019.4 以上

#### インストール
Fancy Carousel Viewは低レイヤー実装として<a href="https://github.com/setchi/FancyScrollView">Fancy Scroll View</a>を使用しています。  
したがってこれら二つをインストールする必要があります。

1. Window > Package ManagerからPackage Managerを開く
2. 「+」ボタン > Add package from git URL
3. 以下を入力してFancy Scroll Viewをインストール
   * https://github.com/setchi/FancyScrollView.git#upm
4. 以下を入力してFancy Carousel Viewをインストール
   * https://github.com/Haruma-K/FancyCarouselView.git?path=/Assets/FancyCarouselView

<p align="center">
  <img width=500 src="https://user-images.githubusercontent.com/47441314/118421190-97842b00-b6fb-11eb-9f94-4dc94e82367a.png" alt="Package Manager">
</p>

あるいはPackages/manifest.jsonを開き、dependenciesブロックに以下を追記します。

```json
{
    "dependencies": {
        "jp.setchi.fancyscrollview": "https://github.com/setchi/FancyScrollView.git#upm",
        "com.harumak.fancycarouselview": "https://github.com/Haruma-K/FancyCarouselView.git?path=/Assets/FancyCarouselView"
    }
}
```

バージョンを指定したい場合には以下のように記述します。

* https://github.com/Haruma-K/FancyCarouselView.git?path=/Assets/FancyCarouselView#1.0.0

## 基本的な使い方

#### セルのためのデータを作成する
まずカルーセルの要素であるセルごとのデータを作成します。  
以下の例では、セルの背景テクスチャを読み込むためのキーとセルに表示する文字列を定義しています。

```cs
public class DemoData
{
    public string SpriteResourceKey { get; }
    public string Text { get; }

    public DemoData(string spriteResourceKey, string text)
    {
        SpriteResourceKey = spriteResourceKey;
        Text = text;
    }
}
```

#### セルを作成する
次にセルのビューを作成します。  
`CarouselCell`クラスを継承し、`Refresh`メソッドにビューの更新処理を記述します。

```cs
using FancyCarouselView.Runtime.Scripts;
using UnityEngine;
using UnityEngine.UI;

public class DemoCarouselCell : CarouselCell<DemoData, DemoCarouselCell>
{
    [SerializeField] private Image _image = default;
    [SerializeField] private Text _text = default;

    protected override void Refresh(DemoData data)
    {
        _image.sprite = Resources.Load<Sprite>(data.SpriteResourceKey);
        _text.text = data.Text;
    }
}
```

これをGameObjectにアタッチし、セルのPrefabを作成しておきます。

#### カルーセルビューを作成する
次にカルーセルビューを作成します。  
上述のデータとセルのクラスをジェネリックの型に指定したCarouselViewクラスを継承したクラスを作成します。

```cs
using FancyCarouselView.Runtime.Scripts;

public class DemoCarouselView : CarouselView<DemoData, DemoCarouselCell>
{
}
```

これをCanvas配下のGameObjectにアタッチします。  
RectTransformの大きさでカルーセルビューの大きさを調整し、CarouselViewの`CellSize`プロパティでセル一個あたりの大きさを調整します。  
また、`Cell Prefab`プロパティには前節で作成したPrefabをアサインします。

#### カルーセルビューを初期化する
ここまでできたら、あとは`CarouseView.Setup`メソッドを用いてデータを渡せばカルーセルが表示されます。

```cs
using System.Linq;
using UnityEngine;

namespace Demo.Scripts
{
    public class Demo : MonoBehaviour
    {
        [SerializeField] private DemoCarouselView _carouselView = default;
        [SerializeField, Range(1, 3)] private int _bannerCount = 3;

        private void Start()
        {
            var items = Enumerable.Range(0, _bannerCount)
                .Select(i =>
                {
                    var spriteResourceKey = $"tex_demo_banner_{i:D2}";
                    var text = $"Demo Banner {i:D2}";
                    return new DemoData(spriteResourceKey, text);
                })
                .ToArray();
            _carouselView.Setup(items);
        }
    }
}
```

## 応用的な使い方

#### Carousel Viewの各プロパティを理解する
Carousel ViewのInspectorで設定できる各プロパティの説明は以下の通りです。

<table>
<thead>
<tr><td colspan="3"><b>プロパティ名</b></td><td><b>説明</b></td></tr>
</thead>
<tbody>
<tr><td colspan="3">Cell Container</td><td>カルーセルビューの領域を表すRectTransform。<br>この領域から出たセルは非表示になり、再利用の対象になります。</td></tr>
<tr><td colspan="3">Cell Prefab</td><td>セルのPrefab。</td></tr>
<tr><td colspan="3">Cell Size</td><td>セルのサイズ。</td></tr>
<tr><td colspan="3">Cell Spacing</td><td>セル同士の間隔。</td></tr>
<tr><td colspan="3">Snap Animation Durataion</td><td>スナップアニメーションの秒数。</td></tr>
<tr><td colspan="3">Snap Animation Type</td><td>スナップアニメーションのイージングタイプ。</td></tr>
<tr><td colspan="3">Auto Scrolling</td><td>一定間隔で自動するクロールするか。</td></tr>
<tr><td></td><td colspan=2>Interval</td><td>自動スクロールの間隔（秒）。</td></tr>
<tr><td></td><td colspan=2>Inverse Direction</td><td>自動スクロールの方向を反対するかどうか。</td></tr>
<tr><td colspan="3">Scroll Direction</td><td>スクロール方向。</td></tr>
<tr><td colspan="3">Loop</td><td>両端でループするかどうか。</td></tr>
<tr><td colspan="3">Draggable</td><td>ドラッグ可能かどうか。</td></tr>
<tr><td colspan="3">Progress View</td><td>進捗を表すビュー。詳細は後述。</td></tr>
<tr><td></td><td colspan=2>Clickable</td><td>Progress Viewをクリックした時にそのインデックスにジャンプするか。</td></tr>
</tbody>
</table>

#### プログレスビューを使用する
Fancy Carousel Viewはカルーセルの進捗を表すプログレスビューをサポートしています。

シンプルなドット表示のプログレスビューは以下の手順で使用できます。

1. `pfb_default_carousel_progress_view.prefab`をシーンにインスタンス化
2. Carousel ViewのProgress Viewプロパティに1.をアサイン

上記のPrefabには`CarouselProgressView`を継承した`DotCarouselProgressView`がアタッチされています。  
`CarouselProgressView`や`ClickableCarouselProgressView`を継承したクラスを独自で実装すれば、任意の挙動をするプログレスビューを作成できます。

また、`DotCarouselProgressView`を使用しつつドットの色やサイズだけを変えたい場合には、  
`DotCarouselProgressView`のInspectorからProgress Element Prefabだけを差し替えることで実現できます。

<p align="center">
  <img width=600 src="https://user-images.githubusercontent.com/47441314/143854073-ce7b5f99-22b8-438d-b6dc-44a2aa1eb52f.gif" alt="Custom Progress View">
</p>

#### セルの動きをカスタムする
`CarouselCell.OnPositionUpdated`をオーバーライドするとセルの動きを独自実装できます。  
以下はセルの動きを独自実装した例です。

```cs
protected override void OnPositionUpdated(float position)
{
    base.OnPositionUpdated(position);

    var trans = transform;
    var pos = position * 2.0f - 1.0f;
    var absPos = Mathf.Abs(pos);
    var cellPosZ = Mathf.Lerp(0.0f, 120.0f, absPos);
    trans.localPosition = new Vector3(trans.localPosition.x, trans.localPosition.y, cellPosZ);
    trans.rotation = Quaternion.AngleAxis(pos * -20.0f, Vector3.up);
}
```

<p align="center">
  <img width=600 src="https://user-images.githubusercontent.com/47441314/136646317-d2138797-024a-44d4-af4c-b3d389972890.gif" alt="Demo">
</p>

#### スクロールビューと併せて使う
スクロールビューのコンテンツとしてカルーセルビューを使う場合、  
Unityの仕様によりカルーセルビューがスクロールビューのドラッグをブロックします。  
すなわち、カルーセルビューをドラッグしてもスクロールビューはスクロールしません。

<p align="center">
  <img width=600 src="https://user-images.githubusercontent.com/47441314/139780467-5678bf8a-fe4b-46d4-a8e6-34c66c24d4f2.gif" alt="Demo">
</p>

このような場合にはカルーセルビューのGameObjectに`Scroll Event Propagator`コンポーネントをアタッチします。  
このコンポーネントはドラッグイベントを適切に親の`ScrollRect`に伝播します。  
結果として以下のようにスクロールビューとカルーセルビューが適切に動作します。

<p align="center">
  <img width=600 src="https://user-images.githubusercontent.com/47441314/139779762-13e992e1-ccc6-4819-a283-9ec5a79ce4e9.gif" alt="Demo">
</p>

## ライセンス
本ソフトウェアはMITライセンスで公開しています。  
ライセンスの範囲内で自由に使っていただいてかまいませんが、
使用の際は以下の著作権表示とライセンス表示が必須となります。

* https://github.com/Haruma-K/FancyCarouselView/blob/master/LICENSE.md

また、本ソフトウェアは以下のソフトウェアがインストールされていることを前提として実装されています（同梱はされていません）。

* [FancyScrollView](https://github.com/setchi/FancyScrollView)

このソフトウェアのライセンスの詳細については [Third Party Notices.md](Third%20Party%20Notices.md) を参照してください。

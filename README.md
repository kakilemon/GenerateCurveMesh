# GenerateCurveMesh

- Unityで３次元の太さのある曲線（ここでは広義のトーラス）のメッシュを動的に生成するサンプルです。
- 使用したバージョン : Unity 2020.2.6f1

## デモ
- SmapleSceneを再生すると最初からシーン内にあるHelixとTorusが生成されます。

![screenshot7 0](https://user-images.githubusercontent.com/62280044/114183322-874f9200-997e-11eb-93ed-1b41271b9b8a.png)

## 新しく曲線を作成する
1. （任意）`CurveFuncBase`を継承したクラスを新たに作り、`CurveFunc`をオーバーライドして曲線の式を定義します。
   - `CureveFunc`の引数は媒介変数`t`と時間変数`time`の２つです。
   - `t`は0から1までの値をとります。0から1にかけて１周するような曲線にしてください。

2. `CurveGenerator`をアタッチしたGameObjectを用意します。
   - `Mesh Renderer`コンポーネントには適当なMaterialを設定しておきます。

3. Inspectorで`CurveGenerator`のpublic変数を設定します。
   - `Radius` : 曲線の半径（太さ）
   - `Seg N` : 緯線方向の分割数（下画像、緑線の本数）
   - `Division` : 経線方向の分割数（下画像、青線の本数）
   - `Curve Func` : 生成する曲線の式のクラス（1.で作成した、あるいは既存の`CurveFuncBase`のサブクラス）
   - `Draw Gizmo` : シーン再生中にGizmoを使ってメッシュの辺を描画する

<img width="366" alt="スクリーンショット 2021-04-09 222203" src="https://user-images.githubusercontent.com/62280044/114186709-245ffa00-9982-11eb-8ae3-8ec2bcade958.png">

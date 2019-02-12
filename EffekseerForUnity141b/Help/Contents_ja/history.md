﻿# 更新履歴

## 1.41b
- Macで3Dモデルが表示されない不具合を修正

## 1.41
- ファイルパスに..が含まれているとエフェクトが再生できない不具合を修正
- VRで使用すると描画がおかしくなる不具合を修正
- Release関数を呼んでも実際はエフェクトが解放されていなかった不具合を修正

## 1.40
- 歪みの仕様が変更された
- 関数の追加

## 1.30
- 歪みの仕様が変更された
- 座標系の仕様が変更された

## 1.23
- [Windows] Unity5.5βで正しく描画できるように対応
- [macOS] OpenGLCoreで正しく描画できるように対応
- インスタンス、四角形のデフォルト数を増加させた

## 1.22
- [Android] 稀に表示されないエフェクトがある不具合を修正
- カリングマスクに対応

## 1.21
- AssetBundleからのロードに対応
- [WebGL] WebGL出力に対応
- 軌跡タイプのエフェクトが描画されないバグを修正
- EffekseerEmitterとHandleにSetTargetLocationを追加
- EffekseerEmitterにpausedとshownを追加
- ヘルプの内容を加筆
- リファレンスマニュアルを追加

## 1.20
- 歪みエフェクトに対応
- EffekseerEmitterとHandleにStopRoot()を追加
- 更新処理をLateUpdateで行うように変更
- テクスチャ解放時の不具合を修正
- [iOS] Metal環境で実行したときにエラーを出力するように変更

## 1.10b
- [Windows] Deferred Renderingで正常に描画できるように修正
- [Mac] Deferred Renderingで正常に描画できるように修正
- [Android] β版追加
- [iOS] β版追加

## 1.10a
- [Windows] x86ビルドで落ちる不具合修正

## 1.10
- リソースの配置場所を StreamingAssets/Effekseer から Resources/Effekseer に変更
- ファイル読み込みにResources.Loadを使用するように変更
- サウンド再生にUnity標準のAudioSourceを使うように変更
- テクスチャロードにUnity標準のTexture2Dを使うように変更

## 1.01
- Unity5.2の新しいネイティブプラグイン仕様に対応

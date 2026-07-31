／／チュートリアル用インターフェイス
public interface ITutorialTask
{
    string Title { get; }  //タスク名
    string Description { get; }  //タスク内容
    float TransitionTime { get; }  //次のタスクに移るまでの時間
    void OnTaskSet();  //タスク開始
    void OnTaskEnd();  //タスク終了
    void Tick();  //条件判定、進行状況の更新など
    bool IsCompleted();  //タスクが完了したかどうか
    string GetProgress();  //現在の進捗
}
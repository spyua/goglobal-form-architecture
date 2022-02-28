using System;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;

namespace Dicon.Util
{
    public static class ObservableUtil
    {
        private static readonly int RETRY_DEFAULT_COUNT = 3;

        public static void AddTo(this IDisposable disposable, CompositeDisposable disposables)
            => disposables.Add(disposable);

        /// <summary>
        /// 在訂閱之前觸發的事件
        /// </summary>
        public static IObservable<T> DoOnSubscribe<T>(this IObservable<T> source, Action action)
        {
            return Observable.Defer(() =>
            {
                action();
                return source;
            });
        }

        /// <summary>
        /// 只發射一條完成通知，或者一條錯誤通知，不可以發射資料，其中完成通知或錯誤通知，兩者只能發生一個
        /// </summary>
        public static IDisposable SubscribeCompletable<T>(this IObservable<T> source, Action<Exception> onError, Action onCompleted)
        {
            return source.Subscribe(_ => { },
                e => onError?.Invoke(e),
                () => onCompleted?.Invoke());
        }

        /// <summary>
        /// 創建發射單一個類別的Observable
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="getNextValue">取得要發射的類別</param>
        /// <param name="onDispose">釋放後的動作</param>
        /// <returns>回傳IObservable</returns>
        public static IObservable<T> CreateSingle<T>(Func<T> getNextValue, Action onDispose = null)
        {
            return Observable.Create<T>(observer =>
            {
                try
                {
                    if (getNextValue == null) throw new NullReferenceException("The getNextValue fucntion is null");
                    observer.OnNext(getNextValue());
                    observer.OnCompleted();
                }
                catch (Exception e)
                {
                    observer.OnError(e);
                }
                return (onDispose == null) ? Disposable.Empty : Disposable.Create(onDispose);
            });
        }

        /// <summary>
        /// 創建預設的Unit，回傳值為Unit.Default
        /// </summary>
        /// <param name="onAction">執行的動作</param>
        /// <param name="onDispose">釋放後的動作</param>
        /// <returns>回傳IObservable</returns>
        public static IObservable<Unit> CreateDefault(Action onAction, Action onDispose = null)
        {
            return CreateSingle(() =>
            {
                onAction?.Invoke();
                return Unit.Default;
            }, onDispose);
        }

        /// <summary>
        /// 創建延遲的時間
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="timeSpan"></param>
        /// <returns></returns>
        public static IObservable<T> CreateSleepDelay<T>(TimeSpan timeSpan)
        {
            return Observable.Empty<T>().Delay(timeSpan);
        }

        /// <summary>
        /// 休息間隔時間
        /// </summary>
        public static IObservable<T> Sleep<T>(this IObservable<T> observable, TimeSpan timeSpan)
        {
            return observable.OnErrorResumeNext(Observable.Empty<T>().Delay(timeSpan));
        }

        /// <summary>
        /// 重複循環及間隔時間
        /// </summary>
        public static IObservable<TSource> DoWhile<TSource>(this IObservable<TSource> source, Func<bool> condition, TimeSpan timeSpan)
        {
            return source.Sleep(timeSpan).DoWhile(condition);
        }

        public static IObservable<TSource> RetryDefault<TSource>(this IObservable<TSource> source)
        {
            return source.Retry(RETRY_DEFAULT_COUNT);
        }

        public static void OnNextDefault(this IObserver<Unit> source) => source.OnNext(Unit.Default);
    }
}

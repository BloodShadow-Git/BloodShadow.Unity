using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BloodShadow.Unity.GameCore.LevelSwitcher
{
    using BloodShadow.Core.Operations;
    using BloodShadow.GameCore.LevelSwitcher;
    using BloodShadow.Unity.Core.Operations;
    using System.Linq;

    public class DefaultLevelSwitcher : LevelSwitcher
    {
        public override void Load(string level, LoadLevelMode mode)
        {
            Debug.Log($"Loading {nameof(level)}: {level}, {nameof(mode)}: {mode}");
            OnLevelStartSwitchAction?.Invoke();
            SceneManager.LoadScene(level, Convert(mode));
            OnLevelSwitchedAction?.Invoke();
            Debug.Log("Done loading");
        }
        public override void AsyncLoad(string level, out Operation operation, bool allowLevelActivation, LoadLevelMode mode, params Operation[] additiveTasks)
        {
            Debug.Log($"Loading async {nameof(level)}: {level}, {nameof(allowLevelActivation)}: {allowLevelActivation}, " +
                $"{nameof(mode)}: {mode}, {nameof(additiveTasks)}.{nameof(additiveTasks.Length)}: {additiveTasks?.Length ?? 0}");
            OnLevelStartSwitchAction?.Invoke();
            operation = new OperationCollection(
                new UnityOperation(SceneManager.LoadSceneAsync(level, Convert(mode))) { AllowSceneActivation = allowLevelActivation }, additiveTasks);
            AsyncLoadTask(operation);
        }
        public override void ForeachLoad(string[] levels)
        {
            Debug.Log($"Loading foreach {nameof(levels)}.{nameof(levels.Length)}: {levels.Length}");
            OnLevelStartSwitchAction?.Invoke();
            foreach (string data in levels) { SceneManager.LoadScene(data, LoadSceneMode.Single); }
            OnLevelSwitchedAction?.Invoke();
        }
        public override void ForeachAsyncLoad(string[] levels, out Operation operation, bool allowLevelActivation, params Operation[] additiveTasks)
        {
            Debug.Log($"Loading foreach async {nameof(levels)}.{nameof(levels.Length)}: {levels.Length}, {nameof(allowLevelActivation)}: {allowLevelActivation}");
            OnLevelStartSwitchAction?.Invoke();
            OperationCollection operationCol = new OperationCollection();
            for (int x = 0; x < levels.Length; x++)
            { operationCol.Add(new UnityOperation(SceneManager.LoadSceneAsync(levels[x], LoadSceneMode.Single)) { AllowSceneActivation = false }); }
            operationCol.Operations.Last().AllowSceneActivation = allowLevelActivation;
            operation = operationCol.Operations.Last();
            ForeachAsyncLoadTask(operationCol);
        }

        private static LoadSceneMode Convert(LoadLevelMode mode)
        {
            return mode switch
            {
                LoadLevelMode.Single => LoadSceneMode.Single,
                LoadLevelMode.Additive => LoadSceneMode.Additive,
                _ => LoadSceneMode.Single
            };
        }
        private async void AsyncLoadTask(Operation operation)
        {
            try
            {
                await operation;
                Debug.Log("Done async loading");
                OnLevelSwitchedAction?.Invoke();
            }
            catch (Exception ex) { Debug.LogError(ex); }
        }
        private async void ForeachAsyncLoadTask(OperationCollection operationCollection)
        {
            try
            {
                for (int i = 0; i < operationCollection.Operations.Count(); i++)
                {
                    if (i >= operationCollection.Operations.Count() - 1)
                    {
                        Debug.Log("Waiting to load last scene");
                        await operationCollection.Operations.ElementAt(i);
                    }

                    await Awaitable.MainThreadAsync();
                    operationCollection.Operations.ElementAt(i).AllowSceneActivation = true;
                    await operationCollection.Operations.ElementAt(i);
                }
                Debug.Log("Done foreach loading");
                OnLevelSwitchedAction?.Invoke();
            }
            catch (Exception ex) { Debug.LogError(ex); }
        }
    }
}

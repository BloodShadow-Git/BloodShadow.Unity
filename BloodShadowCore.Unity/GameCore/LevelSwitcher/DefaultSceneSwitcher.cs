using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BloodShadow.Unity.GameCore.LevelSwitcher
{
    using BloodShadow.Core.Operations;
    using BloodShadow.GameCore.LevelSwitcher;
    using BloodShadow.Unity.Core.Operations;
    using System.Collections.Generic;
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
                new UnityOperation(SceneManager.LoadSceneAsync(level, Convert(mode))) { AllowSceneActivation = allowLevelActivation },
                additiveTasks);
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

        //public override void Load(string scene) { Load(scene, _defaultMode, _defaultSceneSource); }
        //public override void Load(string scene, LoadSceneMode mode) { Load(scene, mode, _defaultSceneSource); }
        //public override void Load(string scene, SceneSource source) { Load(scene, _defaultMode, source); }
        //public override void Load(string scene, LoadSceneMode mode, SceneSource source)
        //{
        //    Debug.Log($"Loading {nameof(scene)}: {scene}, {nameof(mode)}: {mode}, {nameof(source)}: {source}");
        //    OnSceneStartSwitch?.Invoke();
        //    switch (source)
        //    {
        //        default:
        //        case SceneSource.Default:
        //            SceneManager.LoadScene(scene, mode);
        //            break;
        //        case SceneSource.Shortcut:
        //            if (Container.Get(scene, out string loadPath)) { SceneManager.LoadScene(loadPath, mode); }
        //            else { throw new Exception($"Cannot find scene as shortcut: {scene}"); }
        //            break;
        //    }
        //    Debug.Log("Done loading");
        //    OnSceneSwitched?.Invoke();
        //}


        //public override void AsyncLoad(string scene, out Operation operation)
        //{ AsyncLoad(scene, out operation, _defaultSceneActivation, _defaultMode, _defaultSceneSource); }
        //public override void AsyncLoad(string scene, out Operation operation, bool allowSceneActivation)
        //{ AsyncLoad(scene, out operation, allowSceneActivation, _defaultMode, _defaultSceneSource); }
        //public override void AsyncLoad(string scene, out Operation operation, LoadSceneMode mode)
        //{ AsyncLoad(scene, out operation, true, mode, _defaultSceneSource); }
        //public override void AsyncLoad(string scene, out Operation operation, SceneSource source)
        //{ AsyncLoad(scene, out operation, true, _defaultMode, source); }
        //public override void AsyncLoad(string scene, out Operation operation, bool allowSceneActivation, LoadSceneMode mode)
        //{ AsyncLoad(scene, out operation, allowSceneActivation, mode, _defaultSceneSource); }
        //public override void AsyncLoad(string scene, out Operation operation, LoadSceneMode mode, SceneSource source)
        //{ AsyncLoad(scene, out operation, true, mode, source); }
        //public override void AsyncLoad(string scene, out Operation operation, bool allowSceneActivation, SceneSource source)
        //{ AsyncLoad(scene, out operation, allowSceneActivation, _defaultMode, source); }
        //public override void AsyncLoad(string scene, out Operation operation, bool allowSceneActivation, LoadSceneMode mode, SceneSource source)
        //{ AsyncLoad(scene, out operation, allowSceneActivation, mode, source, null); }


        //public override void AsyncLoad(SceneData data, out Operation operation)
        //{ AsyncLoad(data.SceneName, out operation, _defaultSceneActivation, _defaultMode, _defaultSceneSource); }
        //public override void AsyncLoad(SceneData data, out Operation operation, bool allowSceneActivation)
        //{ AsyncLoad(data.SceneName, out operation, allowSceneActivation, _defaultMode, _defaultSceneSource); }
        //public override void AsyncLoad(SceneData data, out Operation operation, LoadSceneMode mode)
        //{ AsyncLoad(data.SceneName, out operation, true, mode, _defaultSceneSource); }
        //public override void AsyncLoad(SceneData data, out Operation operation, SceneSource source)
        //{ AsyncLoad(data.SceneName, out operation, true, _defaultMode, source); }
        //public override void AsyncLoad(SceneData data, out Operation operation, bool allowSceneActivation, LoadSceneMode mode)
        //{ AsyncLoad(data.SceneName, out operation, allowSceneActivation, mode, _defaultSceneSource); }
        //public override void AsyncLoad(SceneData data, out Operation operation, LoadSceneMode mode, SceneSource source)
        //{ AsyncLoad(data.SceneName, out operation, true, mode, source); }
        //public override void AsyncLoad(SceneData data, out Operation operation, bool allowSceneActivation, SceneSource source)
        //{ AsyncLoad(data.SceneName, out operation, allowSceneActivation, _defaultMode, source); }
        //public override void AsyncLoad(SceneData data, out Operation operation, bool allowSceneActivation, LoadSceneMode mode, SceneSource source)
        //{ AsyncLoad(data.SceneName, out operation, allowSceneActivation, mode, source, null); }


        //public override void AsyncLoad(string scene, out Operation operation, bool allowSceneActivation, params Operation[] additiveTasks)
        //{ AsyncLoad(scene, out operation, allowSceneActivation, _defaultMode, _defaultSceneSource, additiveTasks); }
        //public override void AsyncLoad(string scene, out Operation operation, LoadSceneMode mode, params Operation[] additiveTasks)
        //{ AsyncLoad(scene, out operation, true, mode, _defaultSceneSource, additiveTasks); }
        //public override void AsyncLoad(string scene, out Operation operation, SceneSource source, params Operation[] additiveTasks)
        //{ AsyncLoad(scene, out operation, true, _defaultMode, source, additiveTasks); }
        //public override void AsyncLoad(string scene, out Operation operation, bool allowSceneActivation, LoadSceneMode mode, params Operation[] additiveTasks)
        //{ AsyncLoad(scene, out operation, allowSceneActivation, mode, _defaultSceneSource, additiveTasks); }
        //public override void AsyncLoad(string scene, out Operation operation, LoadSceneMode mode, SceneSource source, params Operation[] additiveTasks)
        //{ AsyncLoad(scene, out operation, true, mode, source, additiveTasks); }
        //public override void AsyncLoad(string scene, out Operation operation, bool allowSceneActivation, SceneSource source, params Operation[] additiveTasks)
        //{ AsyncLoad(scene, out operation, allowSceneActivation, _defaultMode, source, additiveTasks); }
        //public override void AsyncLoad(string scene, out Operation operation, bool allowSceneActivation,
        //    LoadSceneMode mode, SceneSource source, params Operation[] additiveTasks)
        //{
        //    Debug.Log($"Loading async {nameof(scene)}: {scene}, {nameof(allowSceneActivation)}: {allowSceneActivation}, " +
        //        $"{nameof(mode)}: {mode}, {nameof(source)}: {source}, {nameof(additiveTasks)}.{nameof(additiveTasks.Length)}: {additiveTasks?.Length ?? 0}");
        //    OnSceneStartSwitch?.Invoke();
        //    switch (source)
        //    {
        //        default:
        //        case SceneSource.Default:
        //            operation = new OperationCollection(
        //                new UnityAsyncOperation(SceneManager.LoadSceneAsync(scene, mode)) { AllowSceneActivation = allowSceneActivation },
        //                additiveTasks);
        //            break;
        //        case SceneSource.Shortcut:
        //            if (Container.Get(scene, out string loadPath))
        //            {
        //                operation = new OperationCollection(
        //                    new UnityAsyncOperation(SceneManager.LoadSceneAsync(loadPath, mode)) { AllowSceneActivation = allowSceneActivation },
        //                    additiveTasks);
        //            }
        //            else { throw new Exception($"Cannot find scene as shortcut: {scene}"); }
        //            break;
        //    }
        //    AsyncLoadTask(operation);
        //}


        //public override void AsyncLoad(SceneData data, out Operation operation, bool allowSceneActivation, params Operation[] additiveTasks)
        //{ AsyncLoad(data.SceneName, out operation, allowSceneActivation, _defaultMode, _defaultSceneSource, additiveTasks); }
        //public override void AsyncLoad(SceneData data, out Operation operation, LoadSceneMode mode, params Operation[] additiveTasks)
        //{ AsyncLoad(data.SceneName, out operation, true, mode, _defaultSceneSource, additiveTasks); }
        //public override void AsyncLoad(SceneData data, out Operation operation, SceneSource source, params Operation[] additiveTasks)
        //{ AsyncLoad(data.SceneName, out operation, true, _defaultMode, source, additiveTasks); }
        //public override void AsyncLoad(SceneData data, out Operation operation, bool allowSceneActivation, LoadSceneMode mode, params Operation[] additiveTasks)
        //{ AsyncLoad(data.SceneName, out operation, allowSceneActivation, mode, _defaultSceneSource, additiveTasks); }
        //public override void AsyncLoad(SceneData data, out Operation operation, LoadSceneMode mode, SceneSource source, params Operation[] additiveTasks)
        //{ AsyncLoad(data.SceneName, out operation, true, mode, source, additiveTasks); }
        //public override void AsyncLoad(SceneData data, out Operation operation, bool allowSceneActivation, SceneSource source, params Operation[] additiveTasks)
        //{ AsyncLoad(data.SceneName, out operation, allowSceneActivation, _defaultMode, source, additiveTasks); }
        //public override void AsyncLoad(SceneData data, out Operation operation, bool allowSceneActivation,
        //    LoadSceneMode mode, SceneSource source, params Operation[] additiveTasks)
        //{ AsyncLoad(data.SceneName, out operation, allowSceneActivation, mode, source, additiveTasks); }


        //public override void ForeachLoad(SceneData[] scenes, SceneSource source)
        //{ ForeachLoad(scenes.ToList().ConvertAll(input => input.SceneName).ToArray(), source); }
        //public override void ForeachLoad(string[] scenes, SceneSource source)
        //{
        //    Debug.Log($"Loading foreach {nameof(scenes)}.{nameof(scenes.Length)}: {scenes.Length}, {nameof(source)}: {source}");
        //    OnSceneStartSwitch?.Invoke();
        //    foreach (string data in scenes) { SceneManager.LoadScene(data, LoadSceneMode.Single); }
        //    OnSceneSwitched?.Invoke();
        //}

        //public override void ForeachAsyncLoad(string[] scenes, out Operation operation, bool allowSceneActivation)
        //{ ForeachAsyncLoad(scenes, out operation, allowSceneActivation, _defaultSceneSource); }
        //public override void ForeachAsyncLoad(string[] scenes, out Operation operation, SceneSource source)
        //{ ForeachAsyncLoad(scenes, out operation, true, _defaultSceneSource); }
        //public override void ForeachAsyncLoad(string[] scenes, out Operation operation, bool allowSceneActivation, SceneSource source)
        //{
        //    Debug.Log($"Loading foreach async {nameof(scenes)}.{nameof(scenes.Length)}: {scenes.Length}, {nameof(allowSceneActivation)}: {allowSceneActivation}, " +
        //        $"{nameof(source)}: {source}");
        //    OnSceneStartSwitch?.Invoke();
        //    OperationCollection operationCol = new();
        //    for (int x = 0; x < scenes.Length; x++)
        //    {
        //        switch (source)
        //        {
        //            default:
        //            case SceneSource.Default:
        //                operationCol.Add(new UnityAsyncOperation(SceneManager.LoadSceneAsync(scenes[x], LoadSceneMode.Single)) { AllowSceneActivation = false });
        //                break;
        //            case SceneSource.Shortcut:
        //                if (Container.Get(scenes[x], out string loadPath))
        //                { operationCol.Add(new UnityAsyncOperation(SceneManager.LoadSceneAsync(loadPath, LoadSceneMode.Single)) { AllowSceneActivation = false }); }
        //                else { throw new Exception($"Cannot find scene as shortcut: {scenes[x]}"); }
        //                break;
        //        }
        //    }
        //    operationCol.Operations[^1].AllowSceneActivation = allowSceneActivation;
        //    operation = operationCol.Operations[^1];
        //    ForeachAsyncLoadTask(operationCol);
        //}

        //public override void ForeachAsyncLoad(SceneData[] scenes, out Operation operation, bool allowSceneActivation)
        //{ ForeachAsyncLoad(scenes.ToList().ConvertAll(input => input.SceneName).ToArray(), out operation, allowSceneActivation); }
        //public override void ForeachAsyncLoad(SceneData[] scenes, out Operation operation, SceneSource source)
        //{ ForeachAsyncLoad(scenes.ToList().ConvertAll(input => input.SceneName).ToArray(), out operation, source); }
        //public override void ForeachAsyncLoad(SceneData[] scenes, out Operation operation, bool allowSceneActivation, SceneSource source)
        //{ ForeachAsyncLoad(scenes.ToList().ConvertAll(input => input.SceneName).ToArray(), out operation, allowSceneActivation, source); }

        //private async void ForeachAsyncLoadTask(OperationCollection operationCollection)
        //{
        //    try
        //    {
        //        for (int i = 0; i < operationCollection.Operations.Count; i++)
        //        {
        //            if (i >= operationCollection.Operations.Count - 1)
        //            {
        //                Debug.Log("Waiting to load last scene");
        //                await operationCollection.Operations[i];
        //            }

        //            await Awaitable.MainThreadAsync();
        //            operationCollection.Operations[i].AllowSceneActivation = true;
        //            await operationCollection.Operations[i];
        //        }
        //        Debug.Log("Done foreach loading");
        //        OnSceneSwitched?.Invoke();
        //    }
        //    catch (Exception ex) { Debug.LogError(ex); }
        //}
        //private async void AsyncLoadTask(Operation operation)
        //{
        //    try
        //    {
        //        await operation;
        //        Debug.Log("Done async loading");
        //        OnSceneSwitched?.Invoke();
        //    }
        //    catch (Exception ex) { Debug.LogError(ex); }
        //}
    }
}

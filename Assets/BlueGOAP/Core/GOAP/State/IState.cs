using System;
using System.Collections;
using System.Collections.Generic;

namespace BlueGOAP
{
    public interface IState
    {
        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        void Set(string key, bool value);
        /// <summary>
        /// 把所提供状态的所有数据覆盖到当前状态
        /// </summary>
        void Set(IState otherState);
        /// <summary>
        /// 读取数据
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        bool Get(string key);
        /// <summary>
        /// 添加数据修改监听
        /// </summary>
        /// <param name="onChange"></param>
        void AddStateChangeListener(Action onChange);
        /// <summary>
        /// 获取两个State同时包含的键值及当前状态键值对应的数据
        /// </summary>
        IState GetSameData(IState otherState);
        /// <summary>
        /// 反转当前state所有的value值
        /// </summary>
        IState InversionValue();
        /// <summary>
        /// 若当前State包含otherState所有的键值对，且对应值都相等，返回true，反之返回false
        /// </summary>
        /// <returns></returns>
        bool ContainState(IState otherState);
        /// <summary>
        /// 获取跟另外一个状态的差异键值集合
        /// </summary>
        /// <param name="otherState"></param>
        /// <returns></returns>
        ICollection<string> GetValueDifferences(IState otherState);
        /// <summary>
        /// 把所提供状态的所有键值进行筛选
        /// 当前状态不存在的就添加进来，存在则忽略
        /// </summary>
        ICollection<string> GetNotExistKeys(IState otherState);
        /// <summary>
        /// 内部数据是否包含所提供key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        bool ContainKey(string key);
        /// <summary>
        /// 获取当前所有键值
        /// </summary>
        /// <returns></returns>
        ICollection<string> GetKeys();
        /// <summary>
        /// 完全复制另一个状态的值
        /// </summary>
        /// <param name="otherState"></param>
        void Copy(IState otherState);
        /// <summary>
        /// 清空数据
        /// </summary>
        void Clear();
    }

    public static class IStateExtend
    {
        public static IState CreateNew(this IState state)
        {
            return new State();
        }
    }
}

using System;
using System.Collections.Generic;
using DG.Tweening;
using Entitas;
using Entitas.Unity;
using Manager;
using UnityEngine;

namespace Game
{
    /// <summary>
    /// 相机控制
    /// </summary>
    public class CameraController : ViewBase, IGameCameraStateListener
    {
        private Dictionary<CameraParent, Transform> _parentDic;
        private CameraMove _cameraMove;
        private Camera _camera;

        public override void Init(Contexts contexts, IEntity entity)
        {
            base.Init(contexts, entity);

            InitParent();
            InitCamera();
            InitFollowPlayer();

            GameEntity gameEntity = (GameEntity) entity;
            gameEntity.AddGameCameraStateListener(this);
        }


        private void InitCamera()
        {
            _camera = transform.GetComponentInChildren<Camera>();

            if (_camera == null)
            {
                Debug.LogError("无法查找到相机");
            }
            else
            {
                _cameraMove = _camera.transform.gameObject.AddComponent<CameraMove>();
            }

            if(_cameraMove == null)
                return;

            Transform parent = null;
            if (DataManager.Single.LevelGamePartIndex == LevelGamePartID.ONE)
            {
                parent = GetCameraParent(CameraParent.START);
            }
            else
            {
                parent = GetCameraParent(CameraParent.IN_GAME);
            }

            if (parent != null) _cameraMove.Init(parent);
        }

        private void InitParent()
        {
            _parentDic = new Dictionary<CameraParent, Transform>();
            Transform temp;

            foreach (CameraParent parent in Enum.GetValues(typeof(CameraParent)))
            {
                temp = transform.Find(parent.ToString());
                if (temp != null)
                {
                    _parentDic[parent] = temp;
                    temp = null;
                }
                else
                {
                    Debug.LogError("无法找到名为：" + parent + " 的相机父物体");
                }
            }
        }

        private void InitFollowPlayer()
        {
            _parentDic[CameraParent.FOLLOW_PLAYER].gameObject.AddComponent<FollowPlayer>();
        }

        private Transform GetCameraParent(CameraParent parent)
        {
            Transform parnetTrans = null;
            _parentDic.TryGetValue(parent, out parnetTrans);
            return parnetTrans;
        }

        public void OnGameCameraState(GameEntity entity, CameraAniName state)
        {
            Transform parnet = null;
            switch (state)
            {
                case CameraAniName.START_GAME_ANI:
                    parnet = GetCameraParent(CameraParent.IN_GAME);
                    if (parnet != null) _cameraMove.Move(parnet, StartAniCallBack);
                    break;
                case CameraAniName.SHAKE_ANI:
                    Shake();
                    break;
                case CameraAniName.FOLLOW_PLAYER:
                    parnet = GetCameraParent(CameraParent.FOLLOW_PLAYER);
                    if (parnet != null) _cameraMove.Move(parnet, null);
                    break;
            }
        }

        private void StartAniCallBack()
        {
            Contexts.sharedInstance.game.ReplaceGameCameraState(CameraAniName.FOLLOW_PLAYER);
        }

        private void Shake()
        {
            if (_camera != null)
            {
                _camera.DOShakePosition(0.2f,0.5f,20);
            }
        }
    }
}

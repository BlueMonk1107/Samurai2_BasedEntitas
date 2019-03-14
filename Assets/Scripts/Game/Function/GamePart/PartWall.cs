using System;
using Game.GamePart;
using Manager;
using UnityEngine;

namespace Game
{
    public class PartWall : MonoBehaviour
    {

        public void Init(LevelGamePartID gamePartId, LevelPartID id, Action spawnEnemy)         
        {
            ZamekEffect[] zameks = InitZamek(transform);

            bool isOpen = JudgeOpenState(gamePartId, id);
            SetOpenState(isOpen, zameks);

            WallCollider[] walls = InitWallCollider(transform);
            SetWallState(isOpen, walls);

            InitStartPartTrigger(walls, zameks, gamePartId, id, spawnEnemy);
        }


        private bool JudgeOpenState(LevelGamePartID gamePartId, LevelPartID id)
        {
            return gamePartId <= DataManager.Single.LevelGamePartIndex
                   && id <= DataManager.Single.LevelPartIndex;
        }

        private ZamekEffect[] InitZamek(Transform wall)
        {
            MeshRenderer[] renderers = wall.GetComponentsInChildren<MeshRenderer>();
            ZamekEffect[] zameks = new ZamekEffect[renderers.Length];

            for (int i = 0; i < renderers.Length; i++)
            {
                zameks[i] = renderers[i].gameObject.AddComponent<ZamekEffect>();
                zameks[i].Init();
            }

            return zameks;
        }

        private void SetOpenState(bool isOpen, ZamekEffect[] zameks)
        {
            foreach (ZamekEffect effect in zameks)
            {
                effect.SetOpenState(isOpen);
            }
        }

        private WallCollider[] InitWallCollider(Transform wall)
        {
            Collider[] colliders = wall.GetComponentsInChildren<Collider>();
            WallCollider[] walls = new WallCollider[colliders.Length];

            for (int i = 0; i < colliders.Length; i++)
            {
                walls[i] = colliders[i].gameObject.AddComponent<WallCollider>();
                walls[i].Init(colliders[i]);
            }

            return walls;
        }

        private void SetWallState(bool isOpen, WallCollider[] walls)
        {
            foreach (WallCollider wall in walls)
            {
                wall.SetWallState(isOpen);
            }
        }

        private void InitStartPartTrigger(WallCollider[] walls, ZamekEffect[] zameks, LevelGamePartID gamePartId, LevelPartID id, Action spawnEnemy)
        {
            StartPartTrigger trigger = transform.parent.gameObject.AddComponent<StartPartTrigger>();
            trigger.Init(() => StartPartGame(walls, zameks, gamePartId, id, spawnEnemy));
        }

        private void StartPartGame(WallCollider[] walls, ZamekEffect[] zameks, LevelGamePartID gamePartId, LevelPartID id, Action spawnEnemy)
        {
            SetOpenState(false, zameks);
            SetWallState(false, walls);
            DataManager.Single.LevelGamePartIndex = gamePartId;
            DataManager.Single.LevelPartIndex = id;

            spawnEnemy?.Invoke();
        }
    }
}

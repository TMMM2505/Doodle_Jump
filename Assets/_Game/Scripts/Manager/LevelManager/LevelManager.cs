using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.Progress;

public class LevelManager : Singleton<LevelManager> 
{
    [SerializeField] private List<Sprite> sprites;
    [SerializeField] private Player player;
    [SerializeField] private Camera camera;
    [SerializeField] private Background background;
    [SerializeField] private GameObject DespawnLineBot;
    /*
    [SerializeField] private Goal prefabGoal;

    [SerializeField] private int numberOfPlatform;
    [SerializeField] private int levelWidth = 2;

    [SerializeField] private List<Platform> platforms = new();
    private List<Hat> hats = new();
    private List<SpringShoes> shoes = new();
    private Vector2 spawnPos = new();
    private Goal goal;

    private bool onStart = true;
    private int currentBg = 0, canJumpPlf;
    private void Awake()
    {
        goal = Instantiate(prefabGoal, new Vector2(0, -2f), Quaternion.identity);
    }
    public void SpawnPlatform(float deltaY, int nPlatform)
    {
        for(int i = 0; i < 20; i++)
        {
            if(onStart)
            {
                spawnPos.x = Random.Range(-levelWidth, levelWidth);
                spawnPos.y = 1f;
                onStart = false;
            }
            else
            {
                spawnPos.x = Random.Range(-levelWidth, levelWidth);
                spawnPos.y += deltaY;
            }

            int type;
            if (canJumpPlf == nPlatform)
            {
                canJumpPlf = 0;
                type = Random.Range(0, 11);
            }
            else
            {
                canJumpPlf++;
                type = Random.Range(0, 10);
            }
            switch (type)
            {
                case 0: case 1: case 2: case 3: case 4: case 5: 
                    {
                        Platform tmp = SimplePool.Spawn<Platform>(PoolType.StaticPlatform, spawnPos, Quaternion.identity);
                        tmp.OnInit(DespawnLineBot);
                        tmp.RemoveFromList = RemovePlatfromFormList;
                        platforms.Add(tmp);

                        int ran = Random.Range(0, 10);
                        if (ran == 1)
                        {
                            SpringShoes springShoes = SimplePool.Spawn<SpringShoes>(PoolType.SpringShoes, tmp.transform.position + new Vector3(0, 0.2f), Quaternion.identity);
                            springShoes.TF.SetParent(tmp.TF);
                            shoes.Add(springShoes);
                        }
                        else if(ran == 2)
                        {
                            Hat hat = SimplePool.Spawn<Hat>(PoolType.Hat, tmp.transform.position + new Vector3(0, 0.3f), Quaternion.identity);
                            hat.TF.SetParent(tmp.TF);
                            hats.Add(hat);
                        }


                        break;
                    }
                case 6: case 7:
                    {
                        DynamicPlatform tmp = SimplePool.Spawn<DynamicPlatform>(PoolType.DynamicPlatform, spawnPos, Quaternion.identity);
                        tmp.OnInit(DespawnLineBot);
                        tmp.RemoveFromList = RemovePlatfromFormList;
                        platforms.Add(tmp);

                        int ran = Random.Range(0, 10);
                        if (ran == 3)
                        {
                            SpringShoes springShoes = SimplePool.Spawn<SpringShoes>(PoolType.SpringShoes, tmp.transform.position + new Vector3(0, 0.2f), Quaternion.identity);
                            springShoes.TF.SetParent(tmp.TF);
                            shoes.Add(springShoes);
                        }
                        else if (ran == 4)
                        {
                            Hat hat = SimplePool.Spawn<Hat>(PoolType.Hat, tmp.transform.position + new Vector3(0, 0.3f), Quaternion.identity);
                            hat.TF.SetParent(tmp.TF);
                            hats.Add(hat);
                        }
                        break;
                    }
                case 8: case 9:
                    {
                        DissappearPlatform tmp = SimplePool.Spawn<DissappearPlatform>(PoolType.DissappearPlatform, spawnPos, Quaternion.identity);
                        tmp.OnInit(DespawnLineBot);
                        tmp.RemoveFromList = RemovePlatfromFormList;
                        platforms.Add(tmp);

                        int ran = Random.Range(0, 10);
                        if (ran == 5)
                        {
                            SpringShoes springShoes = SimplePool.Spawn<SpringShoes>(PoolType.SpringShoes, tmp.transform.position + new Vector3(0, 0.2f), Quaternion.identity);
                            springShoes.TF.SetParent(tmp.TF);
                            shoes.Add(springShoes);
                        }
                        else if (ran == 6)
                        {
                            Hat hat = SimplePool.Spawn<Hat>(PoolType.Hat, tmp.transform.position + new Vector3(0, 0.3f), Quaternion.identity);
                            hat.TF.SetParent(tmp.TF);
                            hats.Add(hat);
                        }
                        break;
                    }
                case 10:
                    {
                        FallingPlatform tmp = SimplePool.Spawn<FallingPlatform>(PoolType.FallingPlatform, spawnPos, Quaternion.identity);
                        tmp.OnInit(DespawnLineBot);
                        tmp.RemoveFromList = RemovePlatfromFormList;
                        platforms.Add(tmp);

                        int ran = Random.Range(0, 10);
                        if (ran == 7)
                        {
                            SpringShoes springShoes = SimplePool.Spawn<SpringShoes>(PoolType.SpringShoes, tmp.transform.position + new Vector3(0, 0.2f), Quaternion.identity);
                            springShoes.TF.SetParent(tmp.TF);
                            shoes.Add(springShoes);
                        }
                        else if (ran == 8)
                        {
                            Hat hat = SimplePool.Spawn<Hat>(PoolType.Hat, tmp.transform.position + new Vector3(0, 0.3f), Quaternion.identity);
                            hat.TF.SetParent(tmp.TF);
                            hats.Add(hat);
                        }
                        break;
                    }
            }
            
        }

    }
    public int checkListPlatforms()
    {
        int dem = 0;
        for(int i = 0; i < platforms.Count; i++)
        {
            if(platforms[i] && platforms[i].gameObject.activeInHierarchy)
            {
                dem++;
            }
        }
        return dem;
    }
    public void SetPLayer()
    {
        background.SetBG(currentBg);
        camera.OnInit();
        player.OnInit(goal.transform.position + new Vector3(0, 1.5f), DespawnLineBot);
    }
    private void Update()
    {
        if(player.GetScore() < 500)
        {
            currentBg = 0;
            background.SetBG(currentBg);
        }
        if (player.GetScore() > 500 && player.GetScore() < 1000)
        {
            currentBg = 1;
            background.SetBG(currentBg);
        }
        else if(player.GetScore() > 1000)
        {
            currentBg = 2;
            background.SetBG(currentBg);
        }
        if (checkListPlatforms() == 10)
        {
            SpawnPlatform(1.5f, 5);
        }
    }

    public void RestLevel()
    {
        onStart = true;
        if(goal)
        {
            goal.gameObject.SetActive(true);
        }
        spawnPos = new();   
        for(int i = 0; i < platforms.Count; i++)
        {
            if (platforms[i])
            {
                platforms[i].ResetPlatform();
                Destroy(platforms[i].gameObject);
                //platforms[i].OnDespawn();
            }
        }
        for(int i = 0; i < hats.Count; i++)
        {
            if (hats[i])
            {
                hats[i].ResetHat();
                hats[i].OnDespawn();
            }
        }
        for(int i = 0; i < shoes.Count; i++)
        {
            if (shoes[i])
            {
                shoes[i].ResetShoes();
                shoes[i].OnDespawn();
            }
        }
    }
    public void RemovePlatfromFormList(Platform platform)
    {
        if(platform != null)
        {
            platforms.Remove(platform);
        }
    }
    public Player GetPlayer() => player;*/

    [SerializeField] private List<Platform> listPlatform = new();
    [SerializeField] private List<Hat> listHat = new();
    [SerializeField] private List<SpringShoes> listShoes = new();

    private Vector2 spawnPosition = new();
    private PlatformType lastType;
    private  Platform platform = new();

    private float maxDeltaY = 3.3f, deltaY;
    private int currentBackground;
    private bool onStart = true;
    public void SpawnPlatform()
{
    for(int i = 0; i < 70; i++)
    {
        if(onStart)
        {
            spawnPosition.y = -3f;
            deltaY = 1.5f;
            onStart = false;
        }
        else
        {
            spawnPosition.y += deltaY;
        }

        spawnPosition.x = Random.Range(-2, 3);
        platform = SimplePool.Spawn<Platform>(PoolType.StaticPlatform, spawnPosition, Quaternion.identity);
        int r2 = Random.Range(1, 4);
        platform.OnInit(DespawnLineBot, (PlatformType) r2, spawnPosition, sprites[r2]);
        platform.DeleteChild();
        platform.RemoveFromList = RemovePlatformFromList;
        listPlatform.Add(platform);

        if (Random.Range(0, 5) % 2 == 0)
        {
            SpawnHat(platform.TF.position + new Vector3(0, 0.3f), platform.TF);
        }
        else
        {
            SpawnShoes(platform.TF.position + new Vector3(0, 0.3f), platform.TF);
        }
    }
}

    public void SpawnHat(Vector2 spawnPos, Transform parent)
    {
        int r = Random.Range(0, 10);
        if(r == 5)
        {
            Hat item = SimplePool.Spawn<Hat>(PoolType.Hat, spawnPos, Quaternion.identity);
            listHat.Add(item);
            item.TF.SetParent(parent);
            item.OnInit(DespawnLineBot);
            item.RemoveFromList = RemoveHatFromList;
        }
    }
    public void SpawnShoes(Vector2 spawnPos, Transform parent)
    {
        int r = Random.Range(0, 10);
        if (r == 5)
        {
            SpringShoes item = SimplePool.Spawn<SpringShoes>(PoolType.SpringShoes, spawnPos, Quaternion.identity);
            listShoes.Add(item);
            item.TF.SetParent(parent);
            item.OnInit(DespawnLineBot);
            item.RemoveFromList = RemoveShoesFromList;
        }
    }
    private void Update()
    {
        if(listPlatform.Count < 50)
        {
            SpawnPlatform();
        }
    }
    public void RemovePlatformFromList(Platform item)
    {
        for (int i = 0; i < listPlatform.Count; i++)
        {
            if (listPlatform[i] == item)
            {
                listPlatform[i].ResetPlatform();
                listPlatform.Remove(item);
            }
        }
    }
    public void RemoveHatFromList(Hat item)
    {
        for (int i = 0; i < listHat.Count; i++)
        {
            if (listHat[i] == item)
            {
                listHat[i].TF.SetParent(null);
                listHat.Remove(item);
            }
        }
    }
    public void RemoveShoesFromList(SpringShoes item)
    {
        for (int i = 0; i < listShoes.Count; i++)
        {
            if (listShoes[i] == item)
            {
                listShoes[i].TF.SetParent(null);
                listShoes.Remove(item);
            }
        }
    }
    public void OnInit()
    {
        player.OnInit(listPlatform[0].TF.position + new Vector3(0, 1.5f), DespawnLineBot);
        camera.OnInit(player.TF.position.y);
        StartCoroutine(ChangeBackGround(20f));
    }
    IEnumerator ChangeBackGround(float time)
    {
        while(true)
        {
            yield return new WaitForSeconds(time);
            if(currentBackground < background.listSprite.Length - 1)
            {
                currentBackground++;
                background.SetBG(currentBackground);
            }
            else
            {
                currentBackground = 0;
                background.SetBG(currentBackground);
            }
            if(deltaY < maxDeltaY)
            {
                deltaY += 0.3f;
            }
        }
    }
    public void ResetLevelMng()
    {
        for(int i = 0; i < listPlatform.Count; i++)
        {
            listPlatform[i].ResetPlatform();
            listPlatform[i].OnDespawn();
        }
        listPlatform.Clear();
        
        for (int i = 0; i < listHat.Count; i++)
        {
            if (listHat[i])
            {
                listHat[i].TF.SetParent(null);
                listHat[i].OnDespawn();
            }
        }
        listHat.Clear();

        for (int i = 0; i < listShoes.Count; i++)
        {
            if (listShoes[i])
            {
                listShoes[i].TF.SetParent(null);
                listShoes[i].OnDespawn();
            }
        }
        listShoes.Clear();

        onStart = true;
        spawnPosition = Vector2.zero;
    }
    public Player GetPlayer() => player;
}

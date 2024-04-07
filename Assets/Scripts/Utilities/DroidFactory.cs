using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class DroidFactory : Singleton<DroidFactory>
{
    [SerializeField] private Droid[] availableDroids;
    [SerializeField] private float waitTime = 180.0f;
    [SerializeField] private int startingDroids = 5;
    [SerializeField] private float minRange = 0.50f;
    [SerializeField] private float maxRange = 15.0f;

    private List<Droid> liveDroids = new List<Droid>();
    private Droid selectedDroid;
    private Player player;

    public List<Droid> LiveDroids
    {
        get { return liveDroids; }
    }
    public Droid SelectedDroid
    {
        get { return selectedDroid; }
    }
    private void Awake()
    {
        Assert.IsNotNull(availableDroids);
    }
    void Start()
    {
        player = GameManager.Instance.CurrentPlayer;
        Assert.IsNotNull(player);
        for (int i = 0; i < startingDroids; i++)
        {
            InstantiateDroid();
        }

        StartCoroutine(GenerateDroids());
    }
    private IEnumerator GenerateDroids()
    {
        while (true)
        {
            InstantiateDroid();
            yield return new WaitForSeconds(waitTime);
        }
    }
    private void InstantiateDroid()
    {
        int index = Random.Range(0, availableDroids.Length);
        float x = player.transform.position.x + GenerateRange() + Random.Range(0.0f, 30.0f);
        float y = player.transform.position.y + 0.25f;
        float z = player.transform.position.z + GenerateRange();
        liveDroids.Add(Instantiate(availableDroids[index], new Vector3(x, y, z), Quaternion.identity));
    }
    public void DroidWasSelected(Droid droid)
    {
        Debug.Log("This droid was selected" + droid);

        selectedDroid = droid;
    }
    private float GenerateRange()
    {
        float randomNum = Random.Range(minRange, maxRange);
        bool isPositive = Random.Range(0, 2) < 4;
        return randomNum;
    }

}
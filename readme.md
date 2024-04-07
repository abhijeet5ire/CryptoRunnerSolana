# CryptoRunner by WalkupLabs

__Copyright (c) 2007 - 2024 CryptoRunner Project by WalkupLabs

A next-gen geo-tagged playground where players meet opportunities offered with a timeless technology. Map-based gameplay with capabilities to engage players and provide market visibility to communities and businesses.

<img src='./Screenshots/homepage.png' />


## How to Install CryptoRunner
1) Download the APK [CryptoRunner](https://drive.google.com/file/d/1IpnzKgYT8NYZs27xprJgEKfQnxw-DXiU/view?usp=sharing) and Install it please provide geolocation and physical activity access to run smoothly

2) Catch all the NFTs


## Aptos OnChain Randomness 
We have used Aptos onchain randomness to place geo tag nfts randomly according to user location 
view[./Assets/Scripts/Utilities/DroidFactory.cs](./Assets/Scripts/Utilities/DroidFactory.cs)
```c#
    if (liveDroids.Count < 2)
    {
        StartCoroutine(FetchRandomLocation((randomNumber) =>
        {
            float x = player.transform.position.x + randomNumber + Random.Range(0.0f, 30.0f);
            float y = player.transform.position.y + 0.25f;
            float z = player.transform.position.z + GenerateRange();
            liveDroids.Add(Instantiate(availableDroids[index], new Vector3(x, y, z), Quaternion.identity));
        }));
    }
    else
    {
        float x = player.transform.position.x + Random.Range(0.0f, 30.0f);
        float y = player.transform.position.y + 0.25f;
        float z = player.transform.position.z + GenerateRange();
        liveDroids.Add(Instantiate(availableDroids[index], new Vector3(x, y, z), Quaternion.identity));
    }
}
private IEnumerator FetchRandomLocation(System.Action<float> callback)
{
    using (UnityWebRequest webRequest = UnityWebRequest.Get(apiUrl))
    {
        yield return webRequest.SendWebRequest();

        if (webRequest.result == UnityWebRequest.Result.ConnectionError ||
            webRequest.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError("Error: " + webRequest.error);
        }
        else
        {
            string response = webRequest.downloadHandler.text;
            Debug.Log("API Response: " + response);

            // Remove the first occurrence of "
            response = response.Replace("\"", "");

            float randomNumber;
            if (float.TryParse(response, out randomNumber))
            {
                Debug.Log("Parsed Random Number: " + randomNumber);
                callback?.Invoke(randomNumber);
            }
            else
            {
                Debug.LogError("Failed to parse API response as float.");
            }
        }
    }
```

which call this function in smart contract
```move
    #[view]
    #[randomness]
    public fun get_geolocation(
        
    ): (u64)  {
       
       let new_roll = randomness::u64_range(0, 6);
        new_roll
    }
```


## Deployed Contract Address
0x1eeeda849696ed6815698f1c9694c730fa338a992def90d4b89963f4994df0d4::geotagrandnft::mint_geo_token

## Screenshot of Project 

<img src='./Screenshots/sucess.jpg' />

<img src='./Screenshots/taptoplay.png' />

<img src='./Screenshots/venture.png' />



// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using System.Linq;

// namespace API
// {
//     [System.Serializable]
//     public class ArtistResponse
//     {
//         public Artist artist;
//         public Artworks[] artworks;
//         public Artworks[] like_artwokrs;
//     }

//     [System.Serializable]
//     public class Artist
//     {
//         public int id;
//         public string name;
//         public string screen_name;
//     }

//     [System.Serializable]
//     public class Artworks
//     {
//         public Artist artist;
//         public Artwork artwork;
//     }

//     [System.Serializable]
//     public class Artwork
//     {
//         public int id;
//         public string media_url;
//     }
// }

// public class GameManager : MonoBehaviour
// {
//     public GameObject artwork;
//     public GameObject proCamera2DTriggerBoundaries;

//     // Start is called before the first frame update
//     IEnumerator Start() // TODO:処理を止めた
//     {
//         // string url = "http://pixel-museum.herokuapp.com/api/v1/artists/pixel_hal/artworks";
//         // string url = "http://pixel-museum.herokuapp.com/api/v1/artists/yuki77mi/artworks";
//         // string url = "http://pixel-museum.herokuapp.com/api/v1/artists/clrfnd/artworks";

//         string url = "";
//         if (SceneParameter.CrossSceneInformation == "") {
//             url = "http://pixel-museum.herokuapp.com/api/v1/artworks";
//         } else {
//             url = $"http://pixel-museum.herokuapp.com/api/v1/artists/{SceneParameter.CrossSceneInformation}/artworks";
//         }

//         var apiwww = new WWW (url);
//         yield return apiwww;

//         var itemJson = apiwww.text;
//         API.ArtistResponse item4 = JsonUtility.FromJson<API.ArtistResponse>(itemJson);

//         var artworkUrls = new string[] {
//             "https://pbs.twimg.com/media/EQ78HO8VAAAC8dk.png",
//             "http://pbs.twimg.com/media/FHFaZREaAAIsq8o.png",
//             "http://pbs.twimg.com/media/FLKsamJaQAQgHSA.jpg",
//             "http://pbs.twimg.com/media/FLgBV1kakAAvdtQ.png",
//             "http://pbs.twimg.com/media/EplgCmdVgAcXIau.png",
//             "http://pbs.twimg.com/media/E6kcWAgUUAA2L8H.png",
//             "http://pbs.twimg.com/media/EsgMaq5VEAE2vb8.png",
//             "https://pbs.twimg.com/profile_banners/1334918399739097089/1609562712",
//             "http://pbs.twimg.com/media/E6eB2d9VUAMPqAI.png",
//             "http://pbs.twimg.com/media/Era42A5VoAEpVVa.png",
//             "http://pbs.twimg.com/media/Eqn9ZOmUYAASTo9.png",
//             "http://pbs.twimg.com/media/EqqXAsKU0AAHSSO.png",
//             "http://pbs.twimg.com/media/EpNDHVkU8AQYp4V.png",
//             "http://pbs.twimg.com/media/E3NE-JrVkAkLOMu.png",
//             "http://pbs.twimg.com/media/EsLgAVJUwAI1p_5.png",
//             "http://pbs.twimg.com/media/Ep7MuISVgAEYmgX.png",
//             "http://pbs.twimg.com/media/FGuuYjgXEAMM8Y1.png",
//             "http://pbs.twimg.com/media/E3iDO_HVUAYmlIF.png",
//             "http://pbs.twimg.com/media/Ez_pVluWEAI8e_O.jpg",
//             "http://pbs.twimg.com/media/E1lRCfGUcAEfpGh.png",
//             "http://pbs.twimg.com/media/E5SJoIAVUAMGPcf.png",
//             "http://pbs.twimg.com/media/FKVgMohaUAEF-4J.png",
//             "http://pbs.twimg.com/media/FEy1JAfaMAAGrb_.png",
//             "http://pbs.twimg.com/media/EzvcBLLVgAUe2_1.png",
//             "http://pbs.twimg.com/media/FC8vz-baAAAY-DM.jpg",
//             "http://pbs.twimg.com/media/E884elSVcBwQNlR.jpg",
//         };

//         // var artworks = item4.like_artwokrs;
//         // var artworks = item4.artworks.Concat(item4.like_artwokrs).ToArray();

//         {
//             var clone = Instantiate(artwork, new Vector3(2, 1.71f, 0), Quaternion.identity);
//             var obj = clone.GetComponent<Artwork>();
//             obj.mediaUrl = "https://pbs.twimg.com/media/FLgBV1kakAAvdtQ?format=png&name=large";
//             obj.artworkInfo = new API.Artworks();
//             obj.artworkInfo.artist = new API.Artist();
//             obj.artworkInfo.artist.screen_name = "yuki77mi";
//             obj.artworkInfo.artwork = new API.Artwork();

//             Instantiate(proCamera2DTriggerBoundaries, new Vector3(2, 1.71f, 0), Quaternion.identity);
//         }

//         var Interval = 10.0f;
//         var i = -1;
//         foreach (var artWorkInfo in item4.artworks)
//         {
//             var positon = new Vector3(i * Interval + 16, 1.71f, 0);
//             var clone = Instantiate(artwork, positon, Quaternion.identity);
//             var obj = clone.GetComponent<Artwork>();
//             obj.mediaUrl = artWorkInfo.artwork.media_url;
//             obj.artworkInfo = artWorkInfo;

//             Instantiate(proCamera2DTriggerBoundaries, positon, Quaternion.identity);

//             i++;
//         }

//         var j = -1;
//         foreach (var artWorkInfo in item4.like_artwokrs)
//         {
//             var positon = new Vector3(j * Interval * -1 - 28, 1.71f, 0);
//             var clone = Instantiate(artwork, positon, Quaternion.identity);
//             var obj = clone.GetComponent<Artwork>();
//             obj.mediaUrl = artWorkInfo.artwork.media_url;
//             obj.artworkInfo = artWorkInfo;

//             Instantiate(proCamera2DTriggerBoundaries, positon, Quaternion.identity);

//             j++;
//         }
//     }

//     // Update is called once per frame
//     void Update()
//     {
        
//     }
// }

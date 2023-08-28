using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BestiaryCharacter : MonoBehaviour
{
   public enum Difficulty
   {
      Easy,
      Medium,
      Hard
   }
   
   public enum Character
   {
      Doll,
      Drum1,
      Drum2,
      Drum3,
      Needle,
      CymbolMonkey
   }

   [Header("Image of Character Entry")] 
   [Space(10)]
   public Image characterImage;
   
   [Header("Type of Character Entry")]
   [Space(10)]
   public Character typeOfCharacter;
   
   [Header("Name of Entry")]
   [Space(10)]
   public string name;

   [Header("Difficulty Level of Entry")]
   [Space(10)]
   public Difficulty dificultyLevel;

   [Header("Description of Entry")]
   [Space(10)]
   public string description;

   [Header("Sounds of the Entry")]
   [Space(10)]
   public List<AudioClip> characterSounds = new List<AudioClip>();
}

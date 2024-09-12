using System;
using AYellowpaper.SerializedCollections;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using NaughtyAttributes;

[Serializable]
[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/RecipesData", order = 1)]
public class RecipesData : ScriptableObject
{
    [SerializedDictionary("Recipe Ingredients", "Recipe Result")] public SerializedDictionary<List<EntityData>, EntityData> _recipes = new SerializedDictionary<List<EntityData>, EntityData>();

    [Button("Save Recipes")] // Specify button text
    private void SaveRecipes()
    {
        SerializedDictionary<List<EntityData>, EntityData> _newRecipes = new SerializedDictionary<List<EntityData>, EntityData>();

        foreach (KeyValuePair<List<EntityData>, EntityData> p in _recipes)
        {
            p.Key.Sort();

            ;
            _newRecipes.Add(p.Key, p.Value);
        }

        _recipes.Clear();
        _recipes = _newRecipes;
    }



    public EntityData GetRecipeResult(List<EntityData> ingredients)
    {
        EntityData Result;

        _recipes.TryGetValue(ingredients, out Result);

        return Result;
    }
}
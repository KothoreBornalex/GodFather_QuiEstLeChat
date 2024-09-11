using System;
using System.Collections;
using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using UnityEngine;



[Serializable]
public enum CursorDesign
{
    Default,
    SimpleCursor,
    HoverCursor,
    InteractCursor,
    DraggedCursor
};

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/CursorData", order = 1)]
public class CursorData : ScriptableObject
{
    [SerializeField] private Vector2 _cursorOffset;
    [SerializedDictionary("Cursor Design", "Cursor Image")] public SerializedDictionary<CursorDesign, Texture2D> _cursors  = new SerializedDictionary<CursorDesign, Texture2D>();

    public Texture2D GetCursor(CursorDesign Design)
    {
        Texture2D Value;

        _cursors.TryGetValue(Design, out Value);

        return Value;
    }

    public Vector2 GetCursorOffset()
    {
        return _cursorOffset;
    }

}

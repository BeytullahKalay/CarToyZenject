using UnityEngine;

public class TerrainPositionCalculation
{
    private readonly BorderSettings.Borders _borders;
    private readonly Terrain _terrain;

    public TerrainPositionCalculation(Terrain terrain, BorderSettings.Borders borders)
    {
        _terrain = terrain;
        _borders = borders;
    }

    public Vector3 GetRandomPositionOnTerrain()
    {
        var position = Vector3.zero;
        
        position.x = Random.Range(_borders.LowerLeft.x, _borders.UpperRight.x);
        position.z = Random.Range(_borders.LowerLeft.y, _borders.UpperRight.y);
        position.y = _terrain.SampleHeight(position);
        
        return position;
    }
}
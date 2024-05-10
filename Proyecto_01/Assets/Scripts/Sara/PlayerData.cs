[System.Serializable]

public class PlayerData //Se puede clase o struct
{
    public float[] posicion = new float[3];

    public PlayerData(PlayerMovement player)
    {
        posicion[0] = player.transform.position.x;
        posicion[1] = player.transform.position.y;
    }
}

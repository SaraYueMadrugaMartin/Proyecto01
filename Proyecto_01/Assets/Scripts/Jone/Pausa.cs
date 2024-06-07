using System; // Para trabajar con eventos

public static class Pausa 
{
    // Eventos para delegar notificaciones de que algo ha sucedido
    public static event Action OnPause; // Action es un m�todo que no toma par�metros y no devuelve un valor
    public static event Action OnResume;

    // M�todo para activar el evento OnPause
    public static void TriggerPause() => OnPause?.Invoke(); // Invoca el evento solo si no es null

    // M�todo para activar el evento OnResume 
    public static void TriggerResume() => OnResume?.Invoke(); // Invoca el evento solo si no es null
}

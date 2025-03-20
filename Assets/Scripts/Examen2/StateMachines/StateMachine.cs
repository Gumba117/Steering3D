public class StateMachine<T>
{
    private State<T> _currentState;

    public void ChangeState(State<T> newState)
    {
        //Existe una funcion Exit para currentstate ?  si => ejeculata
        _currentState?.Exit();
        _currentState = newState;
        //Existe una funcion Enter para newState ?  si => ejeculata

        _currentState?.Enter();
    }
    public void Update()
    {
        _currentState?.Update();
    }
}

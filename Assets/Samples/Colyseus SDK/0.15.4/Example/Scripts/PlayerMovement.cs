using System;
// using SchemaTest.FilteredTypes;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 10f;
    [SerializeField]private bool _moving;
    private NetworkManager _networkManager;
    private Vector2 _targetPosition;

    public async void Start()
    {
        // Initialize game room.
        _networkManager = gameObject.AddComponent<NetworkManager>();
        await _networkManager.JoinOrCreateGame();
        
        // Assigning listener for incoming messages
        _networkManager.GameRoom.OnMessage<string>("welcomeMessage", message =>
        {
            Debug.Log(message);
        });
        
        
        // Set player's new position after synchronized the mouse click's position with the Colyseus server.
        // Something has changed in Schema
        
        // VC added as suggested by Endel
        _networkManager.GameRoom.State.players.OnAdd((key, player) => 
        {
            Debug.Log($"Player {key} has joined the Game!");
            player.OnChange(() => {
                Debug.Log($"{key} has been changed!");
                _targetPosition = new Vector2(player.x, player.y);
                _moving = true;
                Debug.Log("in on change evt " + _targetPosition.ToString());
            });
        });
       
        
        /* VC modified
        _networkManager.GameRoom.State.players.OnChange((key, player) =>
        {
            Debug.Log($"{key} has been changed!");
            _targetPosition = new Vector2(player.x, player.y);
            _moving = true;
            Debug.Log("in on change evt "+_targetPosition.ToString());
        });
        */
        
        /*
        _networkManager.GameRoom.State.OnChange(() =>
        {
            
            var player = _networkManager.GameRoom.State.players[_networkManager.GameRoom.SessionId];
            _targetPosition = new Vector2(player.x, player.y);
            _moving = true;
            Debug.Log("in on change evt "+_targetPosition.ToString()+" state "+_moving);
            
        });
        */
        

        _networkManager.GameRoom.State.players.OnAdd((key, player) =>
        {
            Debug.Log($"Player {key} has joined the Game!");
        });
    }





    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // Synchronize mouse click position with the Colyseus server.
            _networkManager.PlayerPosition(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            Debug.Log("clicked to "+ Camera.main.ScreenToWorldPoint(Input.mousePosition));
        }

        if (_moving)
        {
            Debug.Log("let'smove to "+transform.position.ToString());
        }
        else
        {
            Debug.Log("not moving");    
        }





        if (_moving && (Vector2)transform.position != _targetPosition)
        {
            var step = speed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, _targetPosition, step);
            Debug.Log(transform.position+" moVing!!");
        }
        else
        {
            _moving = false;
        }
    }
}

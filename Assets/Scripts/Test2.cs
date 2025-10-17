using Domain.Gameplay.MessagesDTO;
using Domain.Gameplay.Models.GameState;
using MessagePipe;
using Presentation.Presenters;
using UnityEngine;
using VContainer;

public class Test2 : MonoBehaviour
{
    [Inject] private IPublisher<GameLoadedMessageDto> _publisher;
    [Inject] private IGameStateChanger _gameStateChanger;
    
    private void Start()
    {
        _publisher.Publish(new GameLoadedMessageDto());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            _gameStateChanger.Change(GameState.Building);
    }
}
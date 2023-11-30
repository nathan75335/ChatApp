- ChatAppBackend (ASP.NET Backend)
  - Controllers
    - UserController.cs
    - MessageController.cs
  - Models
    - User.cs
    - Message.cs
  - DataAccess
    - ApplicationDbContext.cs
    - UserRepository.cs
    - MessageRepository.cs
- WpfChatClient (WPF Client)
  - Views
    - Login.xaml
    - ChatWindow.xaml
  - ViewModels
    - LoginViewModel.cs
    - ChatViewModel.cs
- BlazorChatClient (Blazor Client)
  - Pages
    - Index.razor
    - Login.razor
    - Chat.razor
  - Services
    - ApiService.cs

### Dependencies

- ASP.NET Core
- Entity Framework Core
- WPF
- Blazor
- HttpClient
- SignalR or WebSocket

## Contributing

Contributions to this project are welcome! If you encounter issues or have suggestions for improvements, feel free to open an issue or submit a pull request.

## License

This project is licensed under the [MIT License](LICENSE).

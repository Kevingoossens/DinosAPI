using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    // Injection des dépendances UserManager et SignInManager pour gérer les utilisateurs et les connexions
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;

    // Constructeur pour initialiser les services UserManager et SignInManager
    public AuthController(UserManager<User> userManager, SignInManager<User> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    // Route POST pour l'enregistrement d'un nouvel utilisateur
    [HttpPost("Register")]
    public async Task<IActionResult> Register([FromBody] RegisterModel model)
    {
        // Création d'un nouvel utilisateur avec le nom d'utilisateur et l'email fournis
        var user = new User { UserName = model.Username, Email = model.Email };

        // Création de l'utilisateur avec le mot de passe fourni
#pragma warning disable CS8604 // Existence possible d'un argument de référence null.
        var result = await _userManager.CreateAsync(user, model.Password);
#pragma warning restore CS8604 // Existence possible d'un argument de référence null.

        // Si la création de l'utilisateur est réussie, l'utilisateur est connecté et une réponse OK est renvoyée
        if (result.Succeeded)
        {
            await _signInManager.SignInAsync(user, isPersistent: false);
            return Ok();
        }

        // Sinon, renvoie une réponse BadRequest avec les erreurs de création
        return BadRequest(result.Errors);
    }

    // Route POST pour la connexion d'un utilisateur
    [HttpPost("Login")]
    public async Task<IActionResult> Login([FromBody] LoginModel model)
    {
        // Tentative de connexion de l'utilisateur avec le nom d'utilisateur et le mot de passe fournis
#pragma warning disable CS8604 // Existence possible d'un argument de référence null.
        var result = await _signInManager.PasswordSignInAsync(model.Username, model.Password, isPersistent: false, lockoutOnFailure: false);
#pragma warning restore CS8604 // Existence possible d'un argument de référence null.

        // Si la connexion est réussie, renvoie une réponse OK
        if (result.Succeeded)
        {
            return Ok();
        }

        // Sinon, renvoie une réponse Unauthorized pour une connexion échouée
        return Unauthorized();
    }

    // Route POST pour la déconnexion d'un utilisateur
    [HttpPost("Logout")]
    public async Task<IActionResult> Logout()
    {
        // Déconnexion de l'utilisateur courant
        await _signInManager.SignOutAsync();
        return Ok();
    }
}

// Modèle de données pour l'enregistrement d'un utilisateur
public class RegisterModel
{
    public string? Username { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
}

// Modèle de données pour la connexion d'un utilisateur
public class LoginModel
{
    public string? Username { get; set; }
    public string? Password { get; set; }
}
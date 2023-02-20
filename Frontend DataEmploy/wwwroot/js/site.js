function Logout() {
    window.localStorage.clear();
    window.sessionStorage.clear();
    window.location.assign("https://localhost:7104/Login");
}
function Validate() {
    const txtEmail = document.getElementById("txtEmail");
    const emailRegex = /^([\w\.-]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$/;

    if (txtEmail.value == "" || !emailRegex.test(txtEmail.value)) {
        txtEmail.classList.add("is-invalid");
        txtEmail.classList.remove("is-valid"); // Corrected method name to 'remove'
        return false;
    }
}
//no hay return true porque en tal caso procede al postback y si el usuario no se encuentra en la BD, el
// etilo CSS se pierde.

function HideSubMenuContent(event, id) {
    event.preventDefault();
    event.stopPropagation();
    var ulContainer = document.getElementById(id);
    if (ulContainer.classList.contains("hidden")) {
        ulContainer.classList.remove("hidden");
    } else {
        ulContainer.classList.add("hidden");
    }

}
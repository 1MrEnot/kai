function login() {
    let email = $('#loginInEmail').val();
    let password = $('#loginInPassword').val();

    $.ajax({
        type: "POST",
        url: "api/login",
        data: {
            email: email,
            password: password
        },
        success: (res) => {
            window.location = res;
        }
    });
}

function signIn() {
    let email = $('#signInInEmail').val();
    let password = $('#signInInPassword').val();
    let passwordRe = $('#signInInPasswordRe').val();

    if (password !== passwordRe){
        alert("Пароли не совпадают!");
        return;
    }

    if (!email || !password || !passwordRe){
        alert("Введите почту и пароль!");
        return;
    }

    $.post({
        url: "api/signIn",
        data: {
            email: email,
            password: password
        },
        success: (res) => {
            console.log(res);
        }
    });
}

function main(){
    $('#signInButton').click(signIn);
    $('#loginInButton').click(login);
}

$(main);

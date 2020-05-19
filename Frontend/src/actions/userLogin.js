export const userLoginFetch = user => {
    return dispatch =>{
        return fetch("https://localhost:5000/api/Token", {
            method: "POST",
            headers: {
                'Content-Type': 'application/json',
                Accept: 'application/json',
            },
            body: JSON.stringify({user})
        })
        .then(resp => resp.json())
        .then(data => {
            if (data.message)
            {
                console.log(data.message);
            } else {
                localStorage.setItem("token", data.token);
            }
        })
    }
}
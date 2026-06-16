export async function newUserRequest(username){

    const url = "/users/postUser";

    const message = {success: false, error: ""};

    try{

        const response = await fetch(url, {

            method: "POST",
            headers: {

                "Content-Type": "application/json"

            },
            body: JSON.stringify({username: username})

        });

        const data = await response.json()

        if (!data.exists){

            message.success = true;

        }

    }catch(error){

        console.log(error);

        message.error = error;

    }

    return message

}


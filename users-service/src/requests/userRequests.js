export function addNewUser(req, res){

    console.log("request from backend is triggered");


    try{

        const username = req.body.username;

        console.log(username);

        const msg = {exists: false};

        res.json(msg);

    }catch (error){

        console.log(error);

    }

}
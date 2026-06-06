import { useLocation } from "react-router-dom";

export function SearchProducts(){

    const location = useLocation();

    return(

        <div>

            <h2>WELCOME {location.state.currentUser}!</h2>

        </div>

    )


}
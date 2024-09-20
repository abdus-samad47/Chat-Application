import React from "react";
import { UserTable2 } from "../Components/UserTable2";

const UserPage = (prop) => {
    return(
        <div>
            <h1>User Page</h1>
            <UserTable2 username = {prop}/>
        </div>
    )
}

export default UserPage;
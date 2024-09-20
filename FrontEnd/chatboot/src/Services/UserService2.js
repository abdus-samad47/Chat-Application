
export const fetchUsers2 = async (id) => {
    try{
        console.log(typeof(id));
        const userId = id.username.id
        // if (typeof id !== 'string' && typeof id !== 'number') {
        //     throw new Error("Invalid ID format");
        // }
        // const response = await fetch(`http://localhost:5268/api/Users/${encodeURIComponent(id)}`);
        const response = await fetch(`http://localhost:5268/api/Users/${userId}`);
        if(!response.ok){
            throw new Error("Network response was not ok")
        }
        const data = await response.json();
        return data;
    }
    catch(error){
        console.error(error);
        return[];
    }
}
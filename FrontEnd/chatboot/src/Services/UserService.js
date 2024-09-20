
export const fetchUsers = async () => {
    try{
        const response = await fetch('http://localhost:5268/api/Users/');
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
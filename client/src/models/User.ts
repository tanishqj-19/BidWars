export  interface User {
    username : string | null,
    email : string | null,
    password : string | null,
    role : string | null,
    isActive : boolean,
    userId? : number,
}
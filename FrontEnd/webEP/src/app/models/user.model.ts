export class User{
    id?: string;
    userName?: string;
    name?: string;
    surnames?: string;
    typeUser?: string;
    token?: string;
    pathImg?: string;
    constructor(obj?: any){
        Object.assign(this, obj);
    }
}
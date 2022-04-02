export class User{
    constructor(
        public id: string,
        public userName: string,
        public name: string,
        public surnames: string,
        public typeUser: string,
        public token: string,
        public pathImg?: string,
    ){}
}
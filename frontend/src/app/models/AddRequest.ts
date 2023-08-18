export class AddRequest
{
    type: string;
    data: object;

    constructor(type: string, data: object)
    {
        this.type = type;
        this.data = data;
    }
}
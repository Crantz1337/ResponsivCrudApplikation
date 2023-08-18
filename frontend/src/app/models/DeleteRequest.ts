export class DeleteRequest
{
    type: string;
    id: string;

    constructor(type: string, id: string) {
        this.type = type;
        this.id = id;
    }
}
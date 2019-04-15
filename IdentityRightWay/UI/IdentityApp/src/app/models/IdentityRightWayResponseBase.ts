export interface IdentityRightWayResponseBase<T>
{
    totalCount? : number;
    payload : T;
    isValid : boolean;
    errors : string[];
}
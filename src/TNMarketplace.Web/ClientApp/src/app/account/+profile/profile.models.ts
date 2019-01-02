export interface UserInfoModel {
    firstName: string;
    lastName: string;
    phoneNumber: string;
    regionId: number;
    areaId: number;
    location: string;
    isBroker: boolean;
    regions: any[];
}

export interface UpdatePasswordModel {
    oldPassword: string;
    newPassword: string;
    confirmPassword: string;
}

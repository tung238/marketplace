/* SystemJS module definition */
declare var module: NodeModule;
declare var global: NodeJS.Global;
declare module NodeJS {
    interface Global {
        appData: IApplicationConfig
    }
}
interface NodeModule {
    id: string;
}
interface IPictureModel {
    id: number;
    listingID: number;
    url: string;
    ordering: number;
}
interface IApplicationConfig {
    cultures: ICulture[];
    content: StringMap[];
    regionsTree: any[];
    categoriesTree: any[];
    listingTypes: any[];
    loginProviders: string[];
    cookieConsent: ICookieConsent;
}

interface ICulture {
    value: string;
    text: string;
    current: boolean;
}

interface StringMap {
    [s: string]: string;
}
interface ICookieConsent {
    showConsent: boolean;
    cookieString: string;
}
interface KeyValuePair<T> {
    key: string;
    value: T;
}

interface ISocialLogins {
    loginProvider: string;
    providerKey: string;
    providerDisplayName: string;
    active: boolean;
}

interface ITwoFactorModel {
    hasAuthenticator: boolean;

    recoveryCodesLeft: number;

    is2faEnabled: boolean;
}


interface IEnableAuthenticatorModel {
    code: string;
    sharedKey: string;
    authenticatorUri: string;
}
interface ILoginModel {
    username: string;
    password: string;
}

interface IProfileModel {
    sub: string | null;
    jti: string | null;
    useage: string | null;
    at_hash: string | null;
    nbf: number | null;
    exp: number | null;
    iat: number | null;
    iss: string | null;
    unique_name: string | null;
    email_confirmed: boolean;
    role: string[];
    name: string;
}

interface IRegisterModel {
    userName: string;
    password: string;
    confirmPassword: string;
    email: string;
    firstname: string;
    lastname: string;
}

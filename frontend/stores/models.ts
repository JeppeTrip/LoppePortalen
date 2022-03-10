/* tslint:disable */
/* eslint-disable */
//----------------------
// <auto-generated>
//     Generated using the NSwag toolchain v13.10.8.0 (NJsonSchema v10.3.11.0 (Newtonsoft.Json v12.0.0.0)) (http://NSwag.org)
// </auto-generated>
//----------------------
// ReSharper disable InconsistentNaming



export interface CreateOrganiserResponse {
    id: number;
    name?: string | null;
    description?: string | null;
    street?: string | null;
    number?: string | null;
    appartment?: string | null;
    postalCode?: string | null;
    city?: string | null;
}

export interface CreateOrganiserRequest {
    name?: string | null;
    description?: string | null;
    street?: string | null;
    number?: string | null;
    appartment?: string | null;
    postalCode?: string | null;
    city?: string | null;
}

export interface AddContactsToOrganiserResponse {
    organiserId: number;
    contactInformation?: { [key: string]: KeyValuePairOfStringAndContactInfoType; } | null;
}

export interface KeyValuePairOfStringAndContactInfoType {
    key: string;
    value: ContactInfoType;
}

export enum ContactInfoType {
    PHONE_NUMER = 0,
    FACEBOOK = 1,
    TWITTER = 2,
    EMAIL = 3,
    INSTAGRAM = 4,
    TIKTOK = 5,
}

export interface AddContactsToOrganiserRequest {
    organiserId: number;
    contactInformation?: { [key: string]: ContactInfoType; } | null;
}

export interface TestMessage {
    message?: string | null;
}

C:\Users\TripK\Projects\LoppePortalen\backend\Web\NSwag\nswag.extensions.ts
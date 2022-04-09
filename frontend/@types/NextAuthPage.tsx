import { NextPage } from "next"

export type NextPageAuth<P = any, IP = P> = NextPage<P, IP> & {
    requireAuth?: boolean
}
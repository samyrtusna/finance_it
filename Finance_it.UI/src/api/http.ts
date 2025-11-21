import AxiosService from "./axiosInstance";

type MethodType = "get" | "post" | "put" | "delete";

const request = async <
  TResponse,
  TBody = Record<string, unknown> | undefined,
  TParams = Record<string, unknown>
>(
  method: MethodType,
  route: string,
  body?: TBody,
  params?: TParams
): Promise<TResponse> => {
  const response = await AxiosService.request<TResponse>({
    method,
    url: route,
    data: body,
    params,
  });
  return response.data;
};
const Get = <
  TResponse,
  TParams extends Record<string, unknown> = Record<string, unknown>
>(
  route: string,
  params?: TParams
) => request<TResponse, undefined, TParams>("get", route, undefined, params);

const Post = <
  TResponse,
  TBody extends Record<string, unknown> | undefined = undefined,
  TParams extends Record<string, unknown> = Record<string, unknown>
>(
  route: string,
  body?: TBody,
  params?: TParams
) => request<TResponse, TBody, TParams>("post", route, body, params);

const Put = <
  TResponse,
  TBody extends Record<string, unknown>,
  TParams extends Record<string, unknown> = Record<string, unknown>
>(
  route: string,
  body: TBody,
  params?: TParams
) => request<TResponse, TBody, TParams>("put", route, body, params);

const Delete = <
  TResponse,
  TParams extends Record<string, unknown> = Record<string, unknown>
>(
  route: string,
  params?: TParams
) => request<TResponse, undefined, TParams>("delete", route, undefined, params);

const http = { get: Get, post: Post, put: Put, delete: Delete };

export default http;

const isProduction = process.env.NODE_ENV === "production";

const baseApiUrl = isProduction
    ? window.location.origin
    : "https://samples-api";
const apiEndpoint = "/features";

export const env = {
    isProduction,
    apiEndpoint: `${baseApiUrl}${apiEndpoint}`
};

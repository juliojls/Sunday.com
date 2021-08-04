export default class HttpRequest{
    constructor(route, method)
    {
        this.route = route;
        this.method = method;
    }

    setTokem = (tokem) =>{
        this.tokem = tokem;
        return this;
    }

    setBody = (body) =>{
        this.body = JSON.stringify(body)
        return this;
    }

    send = async () => {
        const response = await fetch("/api/" + this.route, {
            method: this.method,
            headers: this.getHeader(),
            body: this.body,
        });

        const responseContent = await response.json();

        return {
            ok: response.ok, 
            errorMessage: response.ok ? null : Array.isArray(responseContent) ? responseContent.join(" ") : responseContent,
            data: response.ok ? responseContent : null
        }

    }

    getHeader = () => {
        const headers = {
            "Content-Type": "application/json",
            "Accept": "application/json" 
        };

        if(this.tokem)
            headers["Authorization"] = "Bearer " + this.tokem;
        
        return headers;

    }
}
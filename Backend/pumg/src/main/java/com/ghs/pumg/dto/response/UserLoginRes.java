package com.ghs.pumg.dto.response;

import com.ghs.pumg.entity.User;
import lombok.Getter;
import lombok.NoArgsConstructor;
import lombok.Setter;

@Getter
@Setter
public class UserLoginRes extends DataResponseBody {
    @Getter
    @NoArgsConstructor
    static class Response {
        private Long idx;
        private String userId;

        private String userNickname;

        public Response(User entity){
            this.idx = entity.getIdx();
            this.userId = entity.getId();
            this.userNickname = entity.getNickName();
        }
    }
    public static UserLoginRes of(Integer statusCode, String message, User userInfo) {
        UserLoginRes res = new UserLoginRes();
        Response response = new Response(userInfo);
        res.setStatus(statusCode);
        res.setMessage(message);
        res.getData().put("userInfo", response);
//        res.getData().put("access-token", accessToken);
//        res.getData().put("sellerIdx", idx);
        return res;
    }
}

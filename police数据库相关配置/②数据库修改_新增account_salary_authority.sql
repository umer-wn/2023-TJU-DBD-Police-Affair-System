-- policemen新增薪水
ALTER TABLE policemen ADD (salary VARCHAR2(6) DEFAULT '5000' NOT NULL);

-- 新建表police_account
CREATE TABLE police_account (
    police_number VARCHAR(20) PRIMARY KEY, 
    police_key VARCHAR(20),
    FOREIGN KEY (police_number) REFERENCES policemen(police_number)
);

-- 将数据从旧表插入新表
INSERT INTO police_account (police_number, police_key)
SELECT police_number, police_key
FROM policemen;

-- 删除policemen中密码
ALTER TABLE policemen
DROP COLUMN police_key;

-- account新增权限
ALTER TABLE police_account ADD authority INT DEFAULT 0;

-- 根据职位修改权限
UPDATE police_account
SET authority =
    CASE
        WHEN EXISTS (
            SELECT 1
            FROM policemen
            WHERE policemen.police_number = police_account.police_number
            AND (policemen.status = '离职' OR policemen.position = '学员')
        ) THEN 0
        WHEN EXISTS (
            SELECT 1
            FROM policemen
            WHERE policemen.police_number = police_account.police_number
            AND policemen.position = '警员'
        ) THEN 1
        WHEN EXISTS (
            SELECT 1
            FROM policemen
            WHERE policemen.police_number = police_account.police_number
            AND policemen.position = '警司'
        ) THEN 2
        WHEN EXISTS (
            SELECT 1
            FROM policemen
            WHERE policemen.police_number = police_account.police_number
            AND policemen.position = '警督'
        ) THEN 3
        WHEN EXISTS (
            SELECT 1
            FROM policemen
            WHERE policemen.police_number = police_account.police_number
            AND policemen.position = '警监'
        ) THEN 4
        WHEN EXISTS (
            SELECT 1
            FROM policemen
            WHERE policemen.police_number = police_account.police_number
            AND policemen.position = '总警监'
        ) THEN 5
    END;

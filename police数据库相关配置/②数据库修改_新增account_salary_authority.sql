-- policemen����нˮ
ALTER TABLE policemen ADD (salary VARCHAR2(6) DEFAULT '5000' NOT NULL);

-- �½���police_account
CREATE TABLE police_account (
    police_number VARCHAR(20) PRIMARY KEY, 
    police_key VARCHAR(20),
    FOREIGN KEY (police_number) REFERENCES policemen(police_number)
);

-- �����ݴӾɱ�����±�
INSERT INTO police_account (police_number, police_key)
SELECT police_number, police_key
FROM policemen;

-- ɾ��policemen������
ALTER TABLE policemen
DROP COLUMN police_key;

-- account����Ȩ��
ALTER TABLE police_account ADD authority INT DEFAULT 0;

-- ����ְλ�޸�Ȩ��
UPDATE police_account
SET authority =
    CASE
        WHEN EXISTS (
            SELECT 1
            FROM policemen
            WHERE policemen.police_number = police_account.police_number
            AND (policemen.status = '��ְ' OR policemen.position = 'ѧԱ')
        ) THEN 0
        WHEN EXISTS (
            SELECT 1
            FROM policemen
            WHERE policemen.police_number = police_account.police_number
            AND policemen.position = '��Ա'
        ) THEN 1
        WHEN EXISTS (
            SELECT 1
            FROM policemen
            WHERE policemen.police_number = police_account.police_number
            AND policemen.position = '��˾'
        ) THEN 2
        WHEN EXISTS (
            SELECT 1
            FROM policemen
            WHERE policemen.police_number = police_account.police_number
            AND policemen.position = '����'
        ) THEN 3
        WHEN EXISTS (
            SELECT 1
            FROM policemen
            WHERE policemen.police_number = police_account.police_number
            AND policemen.position = '����'
        ) THEN 4
        WHEN EXISTS (
            SELECT 1
            FROM policemen
            WHERE policemen.police_number = police_account.police_number
            AND policemen.position = '�ܾ���'
        ) THEN 5
    END;

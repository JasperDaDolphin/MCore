namespace MCore.Base.Rpc {

    /// <summary>
    /// A collection of Rpc events
    /// </summary>
    public static class RpcEvents {
		public const string GetServerInformation = "MCore:client:ready";
		public const string ClientConnect = "MCore:client:connect";
		public const string ClientDisconnect = "MCore:client:disconnect";

		public const string GetUser = "MCore:user:load";
		public const string GetCharacters = "MCore:user:characters";
		public const string AcceptRules = "MCore:user:rules";

		public const string CharacterCreate = "MCore:character:create";
		public const string CharacterLoad = "MCore:character:load";
		public const string CharacterDelete = "MCore:character:delete";
		public const string CharacterSave = "MCore:character:save";
		public const string CharacterRevive = "MCore:character:revive";
		public const string GetCharacterPosition = "MCore:character:getpos";

		public const string CharacterComponentSet = "MCore:character:component:set";
		public const string CharacterPropSet = "MCore:character:prop:set";
		
		public const string BankAtmWithdraw = "MCore:bank:atm:withdraw";
		public const string BankBranchWithdraw = "MCore:bank:branch:withdraw";
		public const string BankBranchDeposit = "MCore:bank:branch:withdraw";
		public const string BankBranchTransfer = "MCore:bank:branch:transfer";
		public const string BankOnlineTransfer = "MCore:bank:online:transfer";

		public const string EntityDelete = "MCore:entity:delete";

		public const string CarCreate = "MCore:car:create";
		public const string CarSpawn = "MCore:car:spawn";
		public const string CarSave = "MCore:car:save";
		public const string CarTransfer = "MCore:car:transfer";
		public const string CarClaim = "MCore:car:claim";
		public const string CarUnclaim = "MCore:car:unclaim";

		public const string BikeSpawn = "MCore:bike:spawn";
		public const string BikeSave = "MCore:bike:save";
		public const string BikeTransfer = "MCore:bike:transfer";
		public const string BikeClaim = "MCore:bike:claim";
		public const string BikeUnclaim = "MCore:bike:unclaim";
	}
}

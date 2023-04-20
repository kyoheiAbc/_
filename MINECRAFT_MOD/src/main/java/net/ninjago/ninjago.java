package net.ninjago;

import javax.swing.text.html.parser.Entity;
import com.mojang.logging.LogUtils;
import net.minecraft.server.level.ServerLevel;
import net.minecraft.world.entity.player.Player;
import net.minecraft.world.item.Items;
import net.minecraft.world.level.Level;
import net.minecraft.world.phys.Vec3;
import net.minecraftforge.common.MinecraftForge;
import net.minecraftforge.event.TickEvent;
import net.minecraftforge.event.entity.player.PlayerInteractEvent;
import net.minecraftforge.eventbus.api.SubscribeEvent;
import net.minecraftforge.fml.common.Mod;
import net.minecraftforge.fml.event.lifecycle.FMLCommonSetupEvent;
import net.ninjago.jitzu.Jitzu;

@Mod.EventBusSubscriber(bus = Mod.EventBusSubscriber.Bus.MOD)
public class ninjago {

    private Jitzu jitzu;

    public static String PL_SKIN = "ninjago:textures/entities/lloyd.png";

    public ninjago() {
        MinecraftForge.EVENT_BUS.register(this);
        jitzu = new Jitzu();
    }

    @SubscribeEvent
    public static void FMLCommonSetupEvent(FMLCommonSetupEvent e) {
        new ninjago();
        new net.ninjago.playerMoel.event();
    }

    @SubscribeEvent
    public void onPlayerTick(TickEvent.PlayerTickEvent e) {
        jitzu.tick(e.player);
    }

    @SubscribeEvent
    public void playerInteractEventLeftClickEmpty(PlayerInteractEvent.LeftClickEmpty e) {
        jitzu.spinGo(e.getEntity(), 20);
    }

    @SubscribeEvent
    public void playerInteractEventRightClickEmpty(PlayerInteractEvent.RightClickEmpty e) {
        jitzu.beamGo(e.getEntity());
    }
}
